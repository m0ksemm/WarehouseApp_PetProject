using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WP.DataAccess.Entities
{
    public class InventoryItem : BaseEntity
    {
        public Guid ProductId { get; set; }

        public Guid StorageLocationId { get; set; }

        public Guid? PalletTypeId { get; set; }

        public int QuantityUnits { get; set; }

        public int ReservedUnits { get; set; }

        public int OccupiedPallets { get; set; }

        public decimal OccupiedAreaM2 { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? LastMovementAt { get; set; }

        public Product Product { get; set; } = null!;

        public StorageLocation StorageLocation { get; set; } = null!;

        public PalletType? PalletType { get; set; }
    }
}
