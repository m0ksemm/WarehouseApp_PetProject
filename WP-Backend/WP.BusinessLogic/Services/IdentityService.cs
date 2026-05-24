using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WP.BusinessLogic.Services.Interfaces;
using WP.DataAccess.ApplicationDb;
using WP.DataAccess.Entities.Permisions;
using WP.Infrastructure.Dtos.Authentication;
using WP.Infrastructure.Enums;

namespace WP.BusinessLogic.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public IdentityService(
            IApplicationRepository<Role> roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Guid[]> GetPermissionsAsync(Guid userId) =>
            await _roleRepository
                .Where(role => role.Users.Any(u => u.Id == userId))
                .SelectMany(role => role.Permissions)
                .Select(permission => permission.Id)
                .Distinct()
                .ToArrayAsync();

        public async Task<IEnumerable<Role>> GetRolesForUserTypeAsync(UserType userType, bool primary = false) =>
            (await _roleRepository
                .Include(r => r.Permissions)
                .Include(r => r.Users)
                .Where(r => r.IsPrimary == primary)
                .ToListAsync())
                .Where(r => r.UserTypes.Any(type => type == userType));



        public async Task<IEnumerable<RoleDto>> GetRolesDtoForUserTypeAsync(UserType userType, bool primary = false) =>
            _mapper.Map<IEnumerable<RoleDto>>(
                await GetRolesForUserTypeAsync(userType, primary)
            );
    }
}
