using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class CategoryConfiguration : BaseEntityTypeConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CategoryTable").HasKey(e => e.Id);

            builder.Property(e => e.CategoryName).HasColumnName("CategoryName").IsRequired();

            builder.Property(e => e.Description).HasColumnName("Description");
        }
    }
}
