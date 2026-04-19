namespace WP.DataAccess.Entities
{
    public class Manufacturer : BaseEntity
    {
        public string ManufacturerName { get; set; }

        public string? Country { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? Address { get; set; }

        public int TotalDeliveries { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
