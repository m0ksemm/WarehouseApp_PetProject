using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WP.DataAccess.Entities;

namespace WP.DataAccess.ApplicationDb.Configurations
{
    public class UserAccountConfiguration : BaseEntityTypeConfiguration<UserAccount>
    {
        public override void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Email);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();

            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users);
        }
    }
}
