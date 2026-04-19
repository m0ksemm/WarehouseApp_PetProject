using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class ProductConfiguration : BaseEntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("ProductTable").HasKey(e => e.Id);

            builder.Property(e => e.ProductName).HasColumnName("ProductName").IsRequired();

            builder.Property(e => e.Description).HasColumnName("Description");

            builder.Property(e => e.SKU).HasColumnName("SKU").IsRequired();

            builder.Property(e => e.BarCode).HasColumnName("BarCode").IsRequired();

            builder.Property(e => e.CategoryId).HasColumnName("CategoryId").IsRequired();

            builder.Property(e => e.ManufacturerId).HasColumnName("ManufacturerId").IsRequired();

            builder.Property(e => e.PackagingProfileId).HasColumnName("PackagingProfileId").IsRequired();

            builder.Property(e => e.WeightKg).HasColumnName("WeightKg");

            builder.Property(e => e.LengthCm).HasColumnName("LengthCm");

            builder.Property(e => e.WidthCm).HasColumnName("WidthCm");

            builder.Property(e => e.HeightCm).HasColumnName("HeightCm");

            builder.Property(e => e.Price).HasColumnName("Price").IsRequired();

            builder.Property(e => e.IsFragile).HasColumnName("IsFragile");

            builder.Property(e => e.RequiresTemperatureControl).HasColumnName("RequiresTemperatureControl");

            builder.Property(e => e.MinStorageTemperature).HasColumnName("MinStorageTemperature");

            builder.Property(e => e.MaxStorageTemperature).HasColumnName("MaxStorageTemperature");

            builder
                .HasOne(e => e.Category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder
                .HasOne(e => e.Manufacturer)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.ManufacturerId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            builder
                .HasOne(e => e.PackagingProfile)
                .WithOne(e => e.Product)
                .HasForeignKey<Product>(e => e.PackagingProfileId)
                .HasPrincipalKey<PackagingProfile>(e => e.Id)
                .IsRequired();
        }
    }
}
