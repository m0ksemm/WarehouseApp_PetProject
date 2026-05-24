namespace WP.Infrastructure.Dtos.Authentication
{
    public class RoleDto
    {
        public string Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsPrimary { get; set; }

        public bool IsExternal { get; set; }

        public ICollection<string> Permissions { get; set; }

        public ICollection<string> UserTypes { get; set; }
    }
}
