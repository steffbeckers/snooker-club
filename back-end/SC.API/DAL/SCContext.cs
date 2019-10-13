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

        public DbSet<Break> Breaks { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<FramePlayer> FramePlayer { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPlayer> GroupPlayer { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeaguePlayer> LeaguePlayer { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerTournament> PlayerTournament { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        // INFO: Users are provided in the base class
        //public DbSet<User> Users { get; set; }

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

            //// Table
            modelBuilder.Entity<League>().ToTable("Leagues");

            //// Properties
            modelBuilder.Entity<League>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<League>().Property(e => e.DisplayName).IsRequired();


            // Tournaments

            //// Soft delete query filter
            modelBuilder.Entity<Tournament>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Tournament>().ToTable("Tournaments");

            //// Properties
            modelBuilder.Entity<Tournament>().Property(e => e.Date).IsRequired();


            // Groups

            //// Soft delete query filter
            modelBuilder.Entity<Group>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Group>().ToTable("Groups");

            //// Properties
            modelBuilder.Entity<Group>().Property(e => e.TournamentId).IsRequired();


            // GroupPlayer

            //// Soft delete query filter
            modelBuilder.Entity<GroupPlayer>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<GroupPlayer>().ToTable("GroupPlayer");

            //// Properties
            modelBuilder.Entity<GroupPlayer>().Property(e => e.GroupId).IsRequired();
            modelBuilder.Entity<GroupPlayer>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<GroupPlayer>().Property(e => e.Position).IsRequired();


            // PlayerTournament

            //// Soft delete query filter
            modelBuilder.Entity<PlayerTournament>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<PlayerTournament>().ToTable("PlayerTournament");

            //// Properties
            modelBuilder.Entity<PlayerTournament>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<PlayerTournament>().Property(e => e.TournamentId).IsRequired();


            // Players

            //// Soft delete query filter
            modelBuilder.Entity<Player>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Player>().ToTable("Players");

            //// Properties
            modelBuilder.Entity<Player>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.LastName).IsRequired();


            // FramePlayer

            //// Soft delete query filter
            modelBuilder.Entity<FramePlayer>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<FramePlayer>().ToTable("FramePlayer");

            //// Properties
            modelBuilder.Entity<FramePlayer>().Property(e => e.FrameId).IsRequired();
            modelBuilder.Entity<FramePlayer>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<FramePlayer>().Property(e => e.Position).IsRequired();


            // Frames

            //// Soft delete query filter
            modelBuilder.Entity<Frame>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Frame>().ToTable("Frames");


            // Breaks

            //// Soft delete query filter
            modelBuilder.Entity<Break>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Break>().ToTable("Breaks");

            //// Properties
            modelBuilder.Entity<Break>().Property(e => e.FrameId).IsRequired();
            modelBuilder.Entity<Break>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<Break>().Property(e => e.Score).IsRequired();


            // Settings

            //// Soft delete query filter
            modelBuilder.Entity<Setting>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Setting>().ToTable("Settings");

            //// Properties
            modelBuilder.Entity<Setting>().Property(e => e.Key).IsRequired();
            modelBuilder.Entity<Setting>().Property(e => e.Value);
        }
    }
}
