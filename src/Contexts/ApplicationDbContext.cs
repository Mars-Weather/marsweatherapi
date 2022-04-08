using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using MarsWeatherApi.Models;

namespace MarsWeatherApi.Contexts {

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sol> Sols { get; set; } = null!;
        public DbSet<Temperature> Temperatures { get; set; } = null!;
        public DbSet<Wind> Winds { get; set; } = null!;
        public DbSet<Pressure> Pressures { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // wind
            modelBuilder.Entity<Sol>()
                .HasOne(a => a.Wind)
                .WithOne(i => i.Sol)
                .HasForeignKey<Wind>(b => b.SolId);

            // temperature
            modelBuilder.Entity<Sol>()
                .HasOne(a => a.Temperature)
                .WithOne(i => i.Sol)
                .HasForeignKey<Temperature>(b => b.SolId);

            // pressure
            modelBuilder.Entity<Sol>()
                .HasOne(a => a.Pressure)
                .WithOne(i => i.Sol)
                .HasForeignKey<Pressure>(b => b.SolId);
        }

    }
}