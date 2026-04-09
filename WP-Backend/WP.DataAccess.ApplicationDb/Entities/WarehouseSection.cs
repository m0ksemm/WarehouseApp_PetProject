using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class WarehouseSection : BaseEntity
    {
        public int WarehouseId { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public SectionType SectionType { get; set; }

        public decimal AreaM2 { get; set; }

        public int MaxPalletCapacity { get; set; }

        public decimal MaxWeightKg { get; set; }

        public bool IsTemperatureControlled { get; set; }

        public decimal? MinTemperature { get; set; }

        public decimal? MaxTemperature { get; set; }

        public Warehouse Warehouse { get; set; } = null!;

        public ICollection<StorageLocation> StorageLocations { get; set; } = new List<StorageLocation>();
    }
}
