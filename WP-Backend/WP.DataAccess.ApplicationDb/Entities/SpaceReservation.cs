using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class SpaceReservation : BaseEntity
    {
        public int TenantId { get; set; }

        public int WarehouseId { get; set; }

        public int? WarehouseSectionId { get; set; }

        public int RequestedPalletPlaces { get; set; }

        public decimal RequestedAreaM2 { get; set; }

        public DateTime ReservationFrom { get; set; }

        public DateTime ReservationTo { get; set; }

        public ReservationStatus Status { get; set; }

        public string? Notes { get; set; }

        public Tenant Tenant { get; set; } = null!;
    }
}
