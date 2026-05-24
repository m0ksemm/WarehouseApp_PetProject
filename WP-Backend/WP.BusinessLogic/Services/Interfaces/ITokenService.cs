using WP.BusinessLogic.Models;
using WP.Infrastructure.Enums;

namespace WP.BusinessLogic.Services.Interfaces
{
    public interface ITokenService
    {
        Task<Token> GenerateTokensAsync(Guid userId, string userName, UserType userType);

        Task DeleteTokenAsync(Guid userId, string refreshToken);

        Task<Token> RefreshTokenWithProxyAsync(string refreshToken, Guid userId, Guid? proxyUserId);

        Task DeleteAllForUserAsync(Guid userId);
    }
}