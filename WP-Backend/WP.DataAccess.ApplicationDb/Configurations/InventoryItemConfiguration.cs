using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class InventoryItemConfiguration : BaseEntityTypeConfiguration<InventoryItem>
    {
        public override void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("InventoryItemsTable", t =>
            {
                t.HasCheckConstraint("CK_Inventory_QuantityUnits_Positive", "[QuantityUnits] > 0");
                t.HasCheckConstraint("CK_Inventory_ReservedUnits_NonNegative", "[ReservedUnits] >= 0");
                t.HasCheckConstraint("CK_Inventory_OccupiedPallets_Positive", "[OccupiedPallets] > 0");
                t.HasCheckConstraint("CK_Inventory_OccupiedArea_Positive", "[OccupiedAreaM2] > 0");
                t.HasCheckConstraint("CK_Inventory_ReservedUnits_LessOrEqual_QuantityUnits", "[ReservedUnits] <= [QuantityUnits]");
                t.HasCheckConstraint("CK_Inventory_ReservedUnits_NonNegative", "[ReservedUnits] >= 0");
            }).HasKey(e => e.Id);

            builder.Property(e => e.ProductId).HasColumnName("ProductId").IsRequired();

            builder.Property(e => e.StorageLocationId).HasColumnName("StorageLocationId").IsRequired();

            builder.Property(e => e.PalletTypeId).HasColumnName("PalletTypeId");

            builder.Property(e => e.QuantityUnits).HasColumnName("QuantityUnits").IsRequired();

            builder.Property(e => e.ReservedUnits).HasColumnName("ReservedUnits").IsRequired();

            builder.Property(e => e.OccupiedPallets).HasColumnName("OccupiedPallets").IsRequired();

            builder.Property(e => e.OccupiedAreaM2).HasColumnName("OccupiedAreaM2").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.BatchNumber).HasColumnName("BatchNumber").HasMaxLength(100);

            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasConversion<DateTime?>();

            builder.Property(e => e.LastMovementAt).HasColumnName("LastMovementAt").HasConversion<DateTime?>();

            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.InventoryItems)
                .HasForeignKey(e => e.ProductId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder
                .HasOne(e => e.StorageLocation)
                .WithMany(e => e.InventoryItems)
                .HasForeignKey(e => e.StorageLocationId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder
                .HasOne(e => e.PalletType)
                .WithMany(e => e.InventoryItems)
                .HasForeignKey(e => e.PalletTypeId)
                .HasPrincipalKey(e => e.Id);
        }

    }
}
