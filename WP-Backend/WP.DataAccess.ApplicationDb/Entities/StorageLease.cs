using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class StorageLease : BaseEntity
    {
        public int TenantId { get; set; }

        public int WarehouseId { get; set; }

        public int? WarehouseSectionId { get; set; }

        public int ReservedPalletPlaces { get; set; }

        public decimal ReservedAreaM2 { get; set; }

        public decimal MonthlyPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LeaseStatus Status { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public Warehouse Warehouse { get; set; } = null!;

        public WarehouseSection? WarehouseSection { get; set; }
    }
}
