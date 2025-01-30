using AutoMapper;
using CarRental.Dto.Rentals;
using CarRental.Model.Entities;
using CarRental.Repository.Cars;
using CarRental.Repository.Rentals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public RentalsController(IRentalRepository rentalRepository, ICarRepository carRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(id);
            if (rental == null)
                return NotFound();

            var rentalDto = _mapper.Map<RentalDto>(rental);

            return Ok(rentalDto);
        }

        [HttpGet("car/{carId}/taken-rentals")]
        public async Task<IActionResult> GetTakenRentalsByCarId(int carId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            if (car == null)
                return NotFound("Car not found");

            var rentals = await _rentalRepository.GetAllRentalsAsync();
            var currentAndFutureRentalsByCar = rentals
                .Where(r => r.CarId == carId && (r.StartDate <= DateTime.Now && r.EndDate >= DateTime.Now || r.StartDate > DateTime.Now))
                .OrderBy(r => r.StartDate)
                .ToList();

            if (!currentAndFutureRentalsByCar.Any())
                return NotFound("No taken rentals found for this car");

            var rentalsDto = _mapper.Map<List<RentalDto>>(currentAndFutureRentalsByCar);

            return Ok(rentalsDto);
        }

        [HttpGet("user/{userId}/rentals")]
        public async Task<IActionResult> GetRentalsByUserId(string userId)
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            var userRentals = rentals
                .Where(r => r.UserId == userId)
                .OrderBy(r => r.StartDate)
                .ToList();

            if (!userRentals.Any())
                return NotFound("No rentals found for this user");

            var rentalsDto = _mapper.Map<List<RentalDto>>(userRentals);

            return Ok(rentalsDto);
        }

        [HttpGet("exists")]
        public async Task<IActionResult> RentalExists(int carId, string userId)
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            var exists = rentals.Any(r => r.CarId == carId && r.UserId == userId);
            return Ok(exists);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            if (rentals == null)
                return NotFound();

            var rentalsDto = _mapper.Map<List<RentalDto>>(rentals);

            return Ok(rentalsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RentalInputDto rental)
        {
            if (rental == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRentals = await _rentalRepository.GetAllRentalsAsync();
            if (existingRentals.Any(r => r.CarId == rental.CarId &&
                                         ((rental.StartDate >= r.StartDate && rental.StartDate <= r.EndDate) ||
                                          (rental.EndDate >= r.StartDate && rental.EndDate <= r.EndDate))))
            {
                return Conflict("The car is already rented in the given period.");
            }

            var car = await _carRepository.GetCarByIdAsync(rental.CarId);
            if (car == null)
                return Conflict("Car not found");

            var totalDays = Math.Ceiling(Math.Abs((rental.StartDate - rental.EndDate).TotalDays));
            var totalPrice = (decimal)totalDays * car.PricePerDay;

            var newRental = new Rental()
            {
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalPrice = totalPrice,
                Status = "Pending",
                CarId = rental.CarId,
                UserId = rental.UserId
            };

            var result = await _rentalRepository.SaveRentalAsync(newRental);
            if (!result)
                throw new Exception("Error saving rental");

            newRental = await _rentalRepository.GetRentalByIdAsync(newRental.Id);

            var rentalDto = _mapper.Map<RentalDto>(newRental);

            return Ok(rentalDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RentalAllInputsDto rental)
        {
            if (rental == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRental = await _rentalRepository.GetRentalByIdAsync(id);
            if (existingRental == null)
                return NotFound();

            existingRental.StartDate = rental.StartDate;
            existingRental.EndDate = rental.EndDate;
            existingRental.TotalPrice = rental.TotalPrice;
            existingRental.Status = rental.Status;
            existingRental.CarId = rental.CarId;
            existingRental.UserId = rental.UserId;

            var result = await _rentalRepository.SaveRentalAsync(existingRental);
            if (!result)
                throw new Exception("Error updating rental");

            var rentalDto = _mapper.Map<RentalDto>(existingRental);

            return Ok(rentalDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRental = await _rentalRepository.GetRentalByIdAsync(id);
            if (existingRental == null)
                return NotFound();

            var result = await _rentalRepository.DeleteRentalAsync(id);
            if (!result)
                throw new Exception("Error deleting rental");

            return Ok();
        }
    }
}
