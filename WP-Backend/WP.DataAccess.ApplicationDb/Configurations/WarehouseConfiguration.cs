using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class WarehouseConfiguration : BaseEntityTypeConfiguration<Warehouse>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("WarehouseTable", t => 
            {
                t.HasCheckConstraint("CK_Total_Area_M2_Positive", "[TotalAreaM2] > 0");

            }).HasKey(e => e.Id);

            builder.Property(e => e.Name).HasColumnName("WarehouseName").HasMaxLength(100).IsRequired();

            builder.Property(e => e.Address).HasColumnName("Address").HasMaxLength(300).IsRequired();

            builder.Property(e => e.TotalAreaM2).HasColumnName("TotalAreaM2").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.SupportsEuropeanPallets).HasColumnName("SupportsEuropeanPallets").IsRequired();

            builder.HasIndex(e => e.Name).IsUnique();
        }
    }
}
