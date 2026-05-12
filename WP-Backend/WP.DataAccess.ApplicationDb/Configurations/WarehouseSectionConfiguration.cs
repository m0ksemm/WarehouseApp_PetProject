using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;
using WP.DataAccess.Extensions;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class WarehouseSectionConfiguration : BaseEntityTypeConfiguration<WarehouseSection>
    {
        public override void Configure(EntityTypeBuilder<WarehouseSection> builder)
        {
            builder.ToTable("WarehouseSectionTable", t => 
            {
                t.HasCheckConstraint("CK_Area_M2_Positive", "[AreaM2] > 0");
                t.HasCheckConstraint("CK_Max_Pallet_Capacity_Positive", "[MaxPalletCapacity] > 0");
                t.HasCheckConstraint("CK_Max_Weight_Kg_Positive", "[MaxWeightKg] > 0");
            }).HasKey(e => e.Id);

            builder.Property(e => e.WarehouseId).HasColumnName("WarehouseId").IsRequired();

            builder.Property(e => e.Code).HasColumnName("Code").IsRequired();

            builder.Property(e => e.Name).HasColumnName("Name").IsRequired();

            builder.Property(e => e.AreaM2).HasColumnName("AreaM2").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.MaxPalletCapacity).HasColumnName("MaxPalletCapacity").IsRequired();

            builder.Property(e => e.MaxWeightKg).HasColumnName("MaxWeightKg").HasWeightConversion().HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.IsTemperatureControlled).HasColumnName("IsTemperatureControlled").IsRequired();

            builder.Property(e => e.MinTemperature).HasColumnName("MinTemperature").HasPrecision(18, 2);

            builder.Property(e => e.MaxTemperature).HasColumnName("MaxTemperature").HasPrecision(18, 2);

            builder.HasOne(e => e.Warehouse)
                .WithMany(w => w.Sections)
                .HasForeignKey(e => e.WarehouseId)
                .IsRequired();
        }
    }
}
