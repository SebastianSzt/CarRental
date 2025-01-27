using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CarRental.Dto.Users;

namespace CarRental.Web.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUserAsync(UserInputDto userInputDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users/register", userInputDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUserAsync(LoginInputDto loginInputDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users/login", loginInputDto);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userId = JsonDocument.Parse(responseContent).RootElement.GetProperty("userId").GetString();
                _httpContextAccessor.HttpContext.Session.SetString("UserId", userId);
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    IsEssential = true
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", userId, cookieOptions);
                return true;
            }
            return false;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/Users/{userId}");
        }

        public void LogoutUser()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserId");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
                if (!string.IsNullOrEmpty(userId))
                {
                    _httpContextAccessor.HttpContext.Session.SetString("UserId", userId);
                }
            }
            return userId;
        }

        public bool IsUserLoggedIn()
        {
            return !string.IsNullOrEmpty(GetUserId());
        }
    }
}
