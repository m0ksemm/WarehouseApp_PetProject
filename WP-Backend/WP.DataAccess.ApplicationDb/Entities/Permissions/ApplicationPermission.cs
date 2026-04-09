namespace WP.DataAccess.Entities.Permisions
{
    public class ApplicationPermission : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<Role> Roles { get; set; }
    }
}
