using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class StorageLocation : BaseEntity
    {
        public int WarehouseSectionId { get; set; }

        public string Code { get; set; } = null!;

        public StorageLocationType LocationType { get; set; }

        public int MaxPallets { get; set; }

        public decimal MaxWeightKg { get; set; }

        public bool IsOccupied { get; set; }

        public WarehouseSection WarehouseSection { get; set; } = null!;

        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
