using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using WP.BusinessLogic.Models;
using WP.BusinessLogic.Services.Interfaces;
using WP.DataAccess.ApplicationDb.Entities;
using WP.DataAccess.Interfaces;
using WP.Infrastructure.Configurations;
using WP.Infrastructure.Enums;

namespace WP.BusinessLogic.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRepository<RefreshToken> _tokenRepository;
        private readonly IIdentityService _identityService;



        public Task DeleteAllForUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTokenAsync(Guid userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GenerateTokensAsync(Guid userId, string userName, UserType userType)
        {
            throw new NotImplementedException();
        }

        public Task<Token> RefreshTokenWithProxyAsync(string refreshToken, Guid userId, Guid? proxyUserId)
        {
            throw new NotImplementedException();
        }
    }
}
