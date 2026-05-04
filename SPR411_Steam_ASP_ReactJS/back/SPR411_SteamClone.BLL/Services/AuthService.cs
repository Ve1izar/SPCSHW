using Microsoft.AspNetCore.Identity;
using SPR411_SteamClone.BLL.Dtos.Auth;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.Services
{
    public class AuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtService _jwtService;
		private readonly EmailService _emailService;

        public AuthService(UserManager<UserEntity> userManager, JwtService jwtService, EmailService emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if(user == null)
            {
                return ServiceResponse.Error($"Користувача з іменем '{dto.UserName}' не існує");
            }

            bool passwordResult = await _userManager.CheckPasswordAsync(user, dto.Password);

            if(!passwordResult)
            {
                return ServiceResponse.Error("Невірний пароль");
            }
			
			var roles = await _userManager.GetRolesAsync(user);

            // Jwt token
            var token = _jwtService.GetAcessToken(user, roles);

            return ServiceResponse.Success("Успішний вхід", token);
        }
		
		public async Task<ServiceResponse> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return ServiceResponse.Error("Користувач з таким Email вже існує");

            if (await _userManager.FindByNameAsync(dto.UserName) != null)
                return ServiceResponse.Error("Користувач з таким UserName вже існує");

            var user = new UserEntity
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return ServiceResponse.Error("Помилка при створенні користувача");

            await _userManager.AddToRoleAsync(user, "user");

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = $"http://localhost:5053/api/auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(emailToken)}";
            
            string emailBody = $"<h1>Вітаємо у Steam Clone!</h1><p>Будь ласка, підтвердіть свою пошту, перейшовши за <a href='{confirmationLink}'>цим посиланням</a>.</p>";
            
            await _emailService.SendEmailAsync(user.Email, "Підтвердження реєстрації", emailBody);

            return ServiceResponse.Success("Реєстрація успішна. Перевірте свою пошту для підтвердження.");
        }
    }
}
