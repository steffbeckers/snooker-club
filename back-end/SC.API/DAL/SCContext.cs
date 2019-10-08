using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SC.API.Models;

namespace SC.API.DAL
{
    public class SCContext : DbContext
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
            // Leagues

            //// Soft delete query filter
            modelBuilder.Entity<League>().HasQueryFilter(e => e.DeletedOn == null);
            
            //// Properties
            modelBuilder.Entity<League>()
                .Property(e => e.Name)
                .IsRequired();
        }
    }
}
