using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class ManufacturerConfiguration : BaseEntityTypeConfiguration<Manufacturer>
    {
        public override void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("ManufacturerTable").HasKey(e => e.Id);

            builder.Property(e => e.ManufacturerName).HasColumnName("ManufacturerName").HasMaxLength(100).IsRequired();

            builder.Property(e => e.Country).HasColumnName("Country").HasMaxLength(150);

            builder.Property(e => e.ContactEmail).HasColumnName("ContactEmail").HasMaxLength(100);

            builder.Property(e => e.ContactPhone).HasColumnName("ContactPhone").HasMaxLength(50);

            builder.Property(e => e.Address).HasColumnName("Address").HasMaxLength(200);

            builder.Property(e => e.TotalDeliveries).HasColumnName("TotalDeliveries");

            builder.HasIndex(e => e.ManufacturerName).IsUnique();
        }
    }
}
