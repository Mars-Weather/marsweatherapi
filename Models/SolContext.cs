using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MarsWeatherApi.Models
{
    public class SolContext : DbContext
    {
        public SolContext(DbContextOptions<SolContext> options)
            : base(options)
        {
        }

        public DbSet<Sol> Sols { get; set; } = null!;
    }
}