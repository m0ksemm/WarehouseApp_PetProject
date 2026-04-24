using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;
using WP.DataAccess.Extensions;
using WP.Infrastructure.Enums;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class PackagingProfileConfiguration : BaseEntityTypeConfiguration<PackagingProfile>
    {
        public override void Configure(EntityTypeBuilder<PackagingProfile> builder) 
        {
            builder.ToTable("PackagingProfilesTable");

            builder.Property(e => e.PackageType).HasColumnName("PackageType").IsRequired();

            builder.Property(e => e.IsWrappedInFilm).HasColumnName("IsWrappedInFilm").IsRequired();

            builder.Property(e => e.IsStackable).HasColumnName("IsStackable").IsRequired();

            builder.Property(e => e.MaxStackCount).HasColumnName("MaximumStackCount");

            builder.Property(e => e.UnitsPerBox).HasColumnName("UnitsPerBox").IsRequired();

            builder.Property(e => e.BoxesPerPallet).HasColumnName("BoxesPerPallet").IsRequired();

            builder.Property(e => e.PackagingWeightKg).HasColumnName("PackagingWeightGrams")
                .HasWeightConversion()
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(e => e.Notes).HasColumnName("Notes");

            builder.HasOne(e => e.Product)
                .WithOne(p => p.PackagingProfile)
                .HasForeignKey<PackagingProfile>(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
