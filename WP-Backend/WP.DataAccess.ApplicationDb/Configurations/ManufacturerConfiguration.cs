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

            builder.Property(e => e.ManufacturerName).HasColumnName("ManufacturerName").IsRequired();

            builder.Property(e => e.Country).HasColumnName("Country");

            builder.Property(e => e.ContactEmail).HasColumnName("ContactEmail");

            builder.Property(e => e.ContactPhone).HasColumnName("ContactPhone");

            builder.Property(e => e.Address).HasColumnName("Address");

            builder.Property(e => e.TotalDeliveries).HasColumnName("TotalDeliveries");
        }
    }
}
