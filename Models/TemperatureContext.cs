using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MarsWeatherApi.Models
{
    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options)
            : base(options)
        {
        }

        public DbSet<Temperature> Temperatures { get; set; } = null!;
    }
}