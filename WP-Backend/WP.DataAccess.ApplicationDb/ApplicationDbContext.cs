using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WP.DataAccess.ApplicationDb.Entities;
using WP.DataAccess.Entities;
using WP.DataAccess.Extensions;

namespace WP.DataAccess.ApplicationDb
{
    public class ApplicationDbContext : IdentityDbContext<UserAccount, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<ResetPasswordAttempt> ResetPasswordAttempts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ConfigureDateTimeConversion();
        }
    }
}
