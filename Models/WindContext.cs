using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MarsWeatherApi.Models
{
    public class WindContext : DbContext
    {
        public WindContext(DbContextOptions<WindContext> options)
            : base(options)
        {
        }

        public DbSet<Wind> Winds { get; set; } = null!;
    }
}