using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class StorageLeaseConfiguration : BaseEntityTypeConfiguration<StorageLease>
    {
        public override void Configure(EntityTypeBuilder<StorageLease> builder)
        {
            builder.ToTable("StorageLeaseTable", t => 
            {
                t.HasCheckConstraint("CK_Requested_Pallet_Places_Positive", "[RequestedPalletPlaces] > 0");
                t.HasCheckConstraint("CK_Requested_Area_M2_Positive", "[RequestedAreaM2] > 0");
                t.HasCheckConstraint("CK_Monthly_Price_Positive", "[MonthlyPrice] > 0");
            }).HasKey(e => e.Id);

            builder.Property(e => e.TenantId).HasColumnName("TenantId").IsRequired();

            builder.Property(e => e.WarehouseId).HasColumnName("WarehouseId").IsRequired();

            builder.Property(e => e.WarehouseSectionId).HasColumnName("WarehouseSectionId").IsRequired();

            builder.Property(e => e.RequestedPalletPlaces).HasColumnName("RequestedPalletPlaces").IsRequired();

            builder.Property(e => e.RequestedAreaM2).HasColumnName("RequestedAreaM2").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.MonthlyPrice).HasColumnName("MonthlyPrice").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.StartDate).HasColumnName("StartDate").IsRequired();

            builder.Property(e => e.EndDate).HasColumnName("EndDate").IsRequired();

            builder.Property(e => e.Status).HasColumnName("LeaseStatus").IsRequired();

            builder.Property(e => e.Notes).HasColumnName("Notes");

            builder.HasOne(e => e.Tenant)
                .WithMany(t => t.StorageLeases)
                .HasForeignKey(e => e.TenantId);

            builder.HasOne(e => e.Warehouse)
                .WithMany(w => w.StorageLeases)
                .HasForeignKey(e => e.WarehouseId);

            builder.HasOne(e => e.WarehouseSection)
                .WithMany(ws => ws.StorageLeases)
                .HasForeignKey(e => e.WarehouseSectionId);
        }
    }
}
