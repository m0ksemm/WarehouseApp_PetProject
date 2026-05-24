using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class TenantConfiguration : BaseEntityTypeConfiguration<Tenant>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("TenantTable").HasKey(e => e.Id);

            builder.Property(e => e.UserAccountId).HasColumnName("UserAccountId").IsRequired();

            builder.Property(e => e.CompanyName).HasColumnName("CompanyName").HasMaxLength(100).IsRequired();

            builder.Property(e => e.ContactPerson).HasColumnName("ContactPerson").HasMaxLength(150);

            builder.Property(e => e.Email).HasColumnName("Email").HasMaxLength(100);

            builder.Property(e => e.Address).HasColumnName("Address").HasMaxLength(300);

            builder.HasOne(e => e.UserAccount)
                .WithOne()
                .HasForeignKey<Tenant>(e => e.UserAccountId)
                .HasPrincipalKey<UserAccount>(e => e.Id)
                .IsRequired();

            builder
                .HasMany(e => e.StorageLeases)
                .WithOne(e => e.Tenant)
                .HasForeignKey(e => e.TenantId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
        }
    }
}
