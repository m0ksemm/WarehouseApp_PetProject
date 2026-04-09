using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities.Permisions
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<UserAccount>();
            Permissions = new HashSet<ApplicationPermission>();
        }

        public Roles Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsPrimary { get; set; }

        public bool IsExternal { get; set; }

        public ICollection<UserAccount> Users { get; set; }

        public ICollection<ApplicationPermission> Permissions { get; set; }
    }
}
