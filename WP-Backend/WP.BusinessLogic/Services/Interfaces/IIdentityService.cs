using WP.DataAccess.Entities.Permisions;
using WP.Infrastructure.Dtos.Authentication;
using WP.Infrastructure.Enums;

namespace WP.BusinessLogic.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Guid[]> GetPermissionsAsync(Guid userId);

        Task<IEnumerable<Role>> GetRolesForUserTypeAsync(UserType userType, bool primary = false);

        Task<IEnumerable<RoleDto>> GetRolesDtoForUserTypeAsync(UserType userType, bool primary = false);
    }
}