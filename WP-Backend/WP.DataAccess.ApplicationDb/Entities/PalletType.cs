using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class PalletType : BaseEntity
    {
        public string PalletName { get; set; } = null!;

        public PalletStandard PalletStandard { get; set; }

        public decimal LengthCm { get; set; }

        public decimal WidthCm { get; set; }

        public decimal MaxLoadKg { get; set; }

        public decimal MaxHeightCm { get; set; }

        public decimal AreaM2 => (LengthCm / 100m) * (WidthCm / 100m);
    }
}
