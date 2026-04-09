namespace WP.DataAccess.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal TotalAreaM2 { get; set; }

        public int MaxPalletCapacity { get; set; }

        public bool SupportsEuropeanPallets { get; set; }

        public bool SupportsAmericanPallets { get; set; }

        public ICollection<WarehouseSection> Sections { get; set; } = new List<WarehouseSection>();
    }
}
