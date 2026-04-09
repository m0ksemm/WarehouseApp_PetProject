namespace WP.DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string SKU { get; set; } = null!;

        public string? BarCode { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ManufacturerId { get; set; }

        public decimal WeightKg { get; set; }

        public decimal LengthCm { get; set; }

        public decimal WidthCm { get; set; }

        public decimal HeightCm { get; set; }

        public decimal Price { get; set; }

        public bool IsFragile { get; set; }

        public bool RequiresTemperatureControl { get; set; }

        public decimal? MinStorageTemperature { get; set; }

        public decimal? MaxStorageTemperature { get; set; }

        public decimal VolumeM3 => (LengthCm / 100m) * (WidthCm / 100m) * (HeightCm / 100m);

        public Category Category { get; set; } = null!;

        public Manufacturer Manufacturer { get; set; } = null!;

        public PackagingProfile? PackagingProfile { get; set; }
    }
}
