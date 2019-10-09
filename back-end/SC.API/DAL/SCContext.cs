using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SC.API.Models;

namespace SC.API.DAL
{
    public class SCContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SCContext (DbContextOptions<SCContext> options) : base(options)
        {
        }

        public DbSet<League> League { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SCContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e => e.ToTable("Users"));
            modelBuilder.Entity<IdentityRole<Guid>>(e => e.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(e =>
            {
                e.ToTable("UserRoles");
                // In case you changed the TKey type
                e.HasKey(key => new { key.UserId, key.RoleId });
            });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e =>
            {
                e.ToTable("UserLogins");
                // In case you changed the TKey type
                e.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(e => e.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(e =>
            {
                e.ToTable("UserTokens");
                // In case you changed the TKey type
                e.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

            // Leagues

            //// Soft delete query filter
            modelBuilder.Entity<League>().HasQueryFilter(e => e.DeletedOn == null);
            
            //// Properties
            modelBuilder.Entity<League>()
                //.ToTable("Leagues")
                .Property(e => e.Name)
                .IsRequired();
        }
    }
}
