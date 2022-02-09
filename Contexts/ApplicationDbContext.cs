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

    }
}