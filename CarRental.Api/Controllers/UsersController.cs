using AutoMapper;
using CarRental.Dto.Users;
using CarRental.Model.Entities;
using CarRental.Repository.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        public UsersController(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users == null)
                return NotFound();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInputDto user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = new User
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = "User",
                PhoneNumber = user.PhoneNumber
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                var saveResult = await _userRepository.SaveUserAsync(newUser);
                if (!saveResult)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error saving user to the database.");
                }

                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputDto login)
        {
            if (login == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (result)
            {
                return Ok(new { UserId = user.Id });
            }

            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserInputDto user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            var passwordHasher = new PasswordHasher<User>();

            existingUser.Email = user.Email;
            existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, user.Password);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.PhoneNumber = user.PhoneNumber;

            var result = await _userRepository.SaveUserAsync(existingUser);
            if (!result)
                throw new Exception("Error updating user");

            var userDto = _mapper.Map<UserDto>(existingUser);

            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            var result = await _userRepository.DeleteUserAsync(id);
            if (!result)
                throw new Exception("Error deleting user");

            return Ok();
        }
    }
}
