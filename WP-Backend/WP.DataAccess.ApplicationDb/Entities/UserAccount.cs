using Microsoft.AspNetCore.Identity;
using WP.DataAccess.Entities.Permisions;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class UserAccount : IdentityUser<Guid>
    {
        public UserAccount()
        {
            Roles = new HashSet<Role>();
            Subordinates = new HashSet<UserAccount>();
        }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public UserType UserType { get; set; }

        public UserStatus UserStatus { get; set; }

        public string? Cellphone { get; set; }

        public string? WorkPhone { get; set; }

        public EmployeePosition? Position { get; set; }

        public int? ManagerId { get; set; }

        public int? TenantId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public UserAccount? Manager { get; set; }

        public ICollection<UserAccount> Subordinates { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
