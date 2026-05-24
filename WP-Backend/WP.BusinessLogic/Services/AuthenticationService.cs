using Microsoft.AspNetCore.Identity;
using WP.BusinessLogic.Models;
using WP.BusinessLogic.Services.Interfaces;
using WP.DataAccess.Entities;
using WP.Infrastructure.Exceptions;
using WP.Infrastructure.Utility;

namespace WP.BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<UserAccount> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Token> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) 
            {
                throw new UnauthorizedException(ExceptionStatusCodeConstants.InvalidCredentials, "Invalid User Name or Password.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid) 
            {
                throw new UnauthorizedException(ExceptionStatusCodeConstants.InvalidCredentials, "Invalid User Name or Password.");
            }

            if ((user.ValidFrom.HasValue && user.ValidFrom > DateTime.UtcNow) || (user.ValidUntil.HasValue && user.ValidUntil < DateTime.UtcNow))
            {
                throw new UnauthorizedException(ExceptionStatusCodeConstants.InvalidCredentials, "User account is not valid at this time.");
            }

            var tokens = await _tokenService.GenerateTokensAsync(user.Id, user.UserName!, user.UserType);

            await SetLoginTimeAsync(user);

            return tokens;
        }

        public async Task LogoutAsync(Guid userId, string refreshToken)
        {
            await _tokenService.DeleteTokenAsync(userId, refreshToken);
        }

        private async Task SetLoginTimeAsync(UserAccount user)
        {
            var norwayTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Oslo");
            user.LastLoginTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, norwayTimeZone);
            await _userManager.UpdateAsync(user);
        }
    }
}
