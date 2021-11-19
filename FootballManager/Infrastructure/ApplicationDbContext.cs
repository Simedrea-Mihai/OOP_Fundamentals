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


            base.OnModelCreating(builder);
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<PlayerAttribute> Attributes { get; set; }

    }
}