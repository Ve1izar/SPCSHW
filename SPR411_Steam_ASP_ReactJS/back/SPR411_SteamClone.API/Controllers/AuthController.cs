using Microsoft.AspNetCore.Mvc;
using SPR411_SteamClone.API.Extensions;
using SPR411_SteamClone.BLL.Dtos.Auth;
using SPR411_SteamClone.BLL.Services;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return this.GetResult(response);
        }
    }
	
	[HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return this.GetResult(response);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            return Ok("Email успішно підтверджено!");
        }
}
