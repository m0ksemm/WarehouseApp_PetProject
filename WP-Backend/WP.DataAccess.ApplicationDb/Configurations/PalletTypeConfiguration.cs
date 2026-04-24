using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;
using WP.DataAccess.Extensions;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class PalletTypeConfiguration : BaseEntityTypeConfiguration<PalletType>
    {
        public override void Configure(EntityTypeBuilder<PalletType> builder) 
        {
            builder.ToTable("PalletTypesTable", t => 
            {
                t.HasCheckConstraint("CK_Pallet_MaxLoad_Positive", "[MaxLoadGrams] > 0");
                t.HasCheckConstraint("CK_Pallet_Length_Positive", "[LengthCm] > 0");
                t.HasCheckConstraint("CK_Pallet_Width_Positive", "[WidthCm] > 0");
            });

            builder.Property(e => e.PalletName).HasColumnName("PalletName").IsRequired();

            builder.Property(e => e.PalletStandard).HasColumnName("PalletStandard").IsRequired();

            builder.Property(e => e.LengthCm).HasColumnName("LengthCm").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.WidthCm).HasColumnName("WidthCm").HasPrecision(18, 2).IsRequired();

            builder.Property(e => e.MaxLoadKg).HasColumnName("MaxLoadGrams").IsRequired().HasWeightConversion();

            builder.Property(e => e.MaxHeightCm).HasColumnName("MaxHeightCm").HasPrecision(18, 2).IsRequired();
        }
    }
}
