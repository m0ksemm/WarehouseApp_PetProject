using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;
using WP.DataAccess.Extensions;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class StorageLocationConfiguration : BaseEntityTypeConfiguration<StorageLocation>
    {
        public override void Configure(EntityTypeBuilder<StorageLocation> builder) 
        {
            builder.ToTable("StorageLocationTable", t => 
            {
                t.HasCheckConstraint("CK_Max_Pallets_Positive", "[MaxPallets] > 0");
                t.HasCheckConstraint("CK_Max_Weight_Kg_Positive", "[MaxWeightKg] > 0");
            }).HasKey(e => e.Id);

            builder.Property(e => e.WarehouseSectionId).HasColumnName("WarehouseSectionId").IsRequired();

            builder.Property(e => e.Code).HasColumnName("Code").IsRequired();

            builder.Property(e => e.LocationType).HasColumnName("LocationType").IsRequired();

            builder.Property(e => e.MaxPallets).HasColumnName("MaxPallets").IsRequired();

            builder.Property(e => e.MaxWeightKg).HasColumnName("MaxWeightKg").HasPrecision(18, 2);

            builder.Property(e => e.IsOccupied).HasColumnName("IsOccupied");

            builder.HasOne(e => e.WarehouseSection)
                .WithMany(p => p.StorageLocations)
                .HasForeignKey(e => e.WarehouseSectionId)
                .IsRequired();

            builder
                .HasMany(e => e.InventoryItems)
                .WithOne(e => e.StorageLocation)
                .HasForeignKey(e => e.StorageLocationId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
        }
    }
}
