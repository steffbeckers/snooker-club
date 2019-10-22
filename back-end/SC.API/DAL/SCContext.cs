using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SC.API.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.DAL
{
    public class SCContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SCContext(DbContextOptions<SCContext> options) : base(options)
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
        public DbSet<PlayerPositionTournament> PlayerPositionTournament { get; set; }
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
            base.OnModelCreating(modelBuilder);

            #region Identity

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

            #endregion

            #region Breaks

            //// Soft delete query filter
            modelBuilder.Entity<Break>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Break>().ToTable("Breaks");

            //// Properties
            modelBuilder.Entity<Break>().Property(e => e.FrameId).IsRequired();
            modelBuilder.Entity<Break>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<Break>().Property(e => e.Score).IsRequired();

            #endregion

            #region FramePlayer

            //// Soft delete query filter
            modelBuilder.Entity<FramePlayer>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<FramePlayer>().ToTable("FramePlayer");

            //// Properties
            modelBuilder.Entity<FramePlayer>().Property(e => e.FrameId).IsRequired();
            modelBuilder.Entity<FramePlayer>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<FramePlayer>().Property(e => e.Position).IsRequired();

            #endregion

            #region Frames

            //// Soft delete query filter
            modelBuilder.Entity<Frame>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Frame>().ToTable("Frames");

            #endregion

            #region GroupPlayer

            //// Soft delete query filter
            modelBuilder.Entity<GroupPlayer>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<GroupPlayer>().ToTable("GroupPlayer");

            //// Properties
            modelBuilder.Entity<GroupPlayer>().Property(e => e.Position).IsRequired();
            modelBuilder.Entity<GroupPlayer>().Property(e => e.GroupId).IsRequired();
            modelBuilder.Entity<GroupPlayer>().Property(e => e.PlayerId).IsRequired();

            #endregion

            #region Groups

            //// Soft delete query filter
            modelBuilder.Entity<Group>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Group>().ToTable("Groups");

            //// Properties
            modelBuilder.Entity<Group>().Property(e => e.TournamentId).IsRequired();

            #endregion

            #region LeaguePlayer

            //// Soft delete query filter
            modelBuilder.Entity<LeaguePlayer>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<LeaguePlayer>().ToTable("LeaguePlayer");

            //// Properties
            modelBuilder.Entity<LeaguePlayer>().Property(e => e.LeagueId).IsRequired();
            modelBuilder.Entity<LeaguePlayer>().Property(e => e.PlayerId).IsRequired();

            #endregion

            #region Leagues

            //// Soft delete query filter
            modelBuilder.Entity<League>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<League>().ToTable("Leagues");

            //// Properties
            modelBuilder.Entity<League>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<League>().Property(e => e.DisplayName).IsRequired();

            #endregion

            #region Players

            //// Soft delete query filter
            modelBuilder.Entity<Player>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Player>().ToTable("Players");

            //// Properties
            modelBuilder.Entity<Player>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<Player>().Property(e => e.LastName).IsRequired();

            #endregion

            #region PlayerPositionTournament

            //// Soft delete query filter
            modelBuilder.Entity<PlayerPositionTournament>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<PlayerPositionTournament>().ToTable("PlayerPositionTournament");

            //// Properties
            modelBuilder.Entity<PlayerPositionTournament>().Property(e => e.Position).IsRequired();
            modelBuilder.Entity<PlayerPositionTournament>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<PlayerPositionTournament>().Property(e => e.TournamentId).IsRequired();

            #endregion

            #region PlayerTournament

            //// Soft delete query filter
            modelBuilder.Entity<PlayerTournament>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<PlayerTournament>().ToTable("PlayerTournament");

            //// Properties
            modelBuilder.Entity<PlayerTournament>().Property(e => e.PlayerId).IsRequired();
            modelBuilder.Entity<PlayerTournament>().Property(e => e.TournamentId).IsRequired();

            #endregion

            #region Settings

            //// Soft delete query filter
            modelBuilder.Entity<Setting>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Setting>().ToTable("Settings");

            //// Properties
            modelBuilder.Entity<Setting>().Property(e => e.Key).IsRequired();
            modelBuilder.Entity<Setting>().Property(e => e.Value);

            #endregion

            #region Tournaments

            //// Soft delete query filter
            modelBuilder.Entity<Tournament>().HasQueryFilter(e => e.DeletedOn == null);

            //// Table
            modelBuilder.Entity<Tournament>().ToTable("Tournaments");

            //// Properties
            modelBuilder.Entity<Tournament>().Property(e => e.Date).IsRequired();

            #endregion
        }

        public override int SaveChanges()
        {
            SoftDeleteLogic();
            TimeStampsLogic();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SoftDeleteLogic();
            TimeStampsLogic();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SoftDeleteLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (entry.Entity.GetType() == typeof(Break) ||
                    entry.Entity.GetType() == typeof(Frame) ||
                    entry.Entity.GetType() == typeof(FramePlayer) ||
                    entry.Entity.GetType() == typeof(Group) ||
                    entry.Entity.GetType() == typeof(GroupPlayer) ||
                    entry.Entity.GetType() == typeof(League) ||
                    entry.Entity.GetType() == typeof(LeaguePlayer) ||
                    entry.Entity.GetType() == typeof(Player) ||
                    entry.Entity.GetType() == typeof(PlayerTournament) ||
                    entry.Entity.GetType() == typeof(Setting) ||
                    entry.Entity.GetType() == typeof(Tournament))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["DeletedOn"] = null;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["DeletedOn"] = DateTime.Now;
                            break;
                    }
                }
            }
        }

        private void TimeStampsLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (entry.Entity.GetType() == typeof(Break) ||
                    entry.Entity.GetType() == typeof(Frame) ||
                    entry.Entity.GetType() == typeof(FramePlayer) ||
                    entry.Entity.GetType() == typeof(Group) ||
                    entry.Entity.GetType() == typeof(GroupPlayer) ||
                    entry.Entity.GetType() == typeof(League) ||
                    entry.Entity.GetType() == typeof(LeaguePlayer) ||
                    entry.Entity.GetType() == typeof(Player) ||
                    entry.Entity.GetType() == typeof(PlayerTournament) ||
                    entry.Entity.GetType() == typeof(Setting) ||
                    entry.Entity.GetType() == typeof(Tournament))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedOn"] = DateTime.Now;
                            entry.CurrentValues["ModifiedOn"] = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues["ModifiedOn"] = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
