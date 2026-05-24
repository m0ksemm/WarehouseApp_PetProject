using WP.BusinessLogic.Models;

namespace WP.BusinessLogic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Token> AuthenticateAsync(string username, string password);

        Task LogoutAsync(Guid userId, string refreshToken);
    }
}
