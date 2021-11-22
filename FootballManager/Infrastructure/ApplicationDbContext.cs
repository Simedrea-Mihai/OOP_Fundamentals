using Domain;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var teams = builder.Entity<Team>();
            teams.ToTable("Teams");
            teams.Property(t => t.Name).IsRequired();
            teams.Property(t => t.Name).HasMaxLength(20);

            var players = builder.Entity<Player>();
            players.ToTable("Players");
            players.Property(p => p.MarketValue).IsRequired();

            var profile = builder.Entity<Profile>();
            profile.ToTable("Profile");
            profile.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            profile.Property(p => p.LastName).IsRequired().HasMaxLength(50);

            builder.Entity<Profile>().Property(p => p.PlayerId).IsRequired(false);
            builder.Entity<Profile>().Property(p => p.ManagerId).IsRequired(false);

            builder.Entity<Player>()
                .HasOne(a => a.Profile)
                .WithOne(a => a.Player)
                .HasForeignKey<Profile>(c => c.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Manager>()
                .HasOne(a => a.Profile)
                .WithOne(a => a.Manager)
                .HasForeignKey<Profile>(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Team>()
                .HasOne(a => a.Manager)
                .WithOne(a => a.Team)
                .HasForeignKey<Manager>(c => c.TeamIdManager);

            base.OnModelCreating(builder);
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<PlayerAttribute> Attributes { get; set; }

    }
}