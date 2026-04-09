using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.Entities
{
    public class PackagingProfile : BaseEntity
    {
        public Guid ProductId { get; set; }

        public PackageType PackageType { get; set; }

        public bool IsWrappedInFilm { get; set; }

        public bool IsStackable { get; set; }

        public int? MaxStackCount { get; set; }

        public int UnitsPerBox { get; set; }

        public int BoxesPerPallet { get; set; }

        public decimal PackagingWeightKg { get; set; }

        public string? Notes { get; set; }

        public Product Product { get; set; } = null!;
    }
}
