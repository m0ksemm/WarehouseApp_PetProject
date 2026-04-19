namespace WP.DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
