using Xunit;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;

namespace unittests;

public class DbUnitTest
{
    private readonly ApplicationDbContext _context;

    public DbUnitTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        string connectionstring = "server=localhost\\sqlexpress;database=marsweatherapidb;trusted_connection=true";
        optionsBuilder.UseSqlServer(connectionstring);
        _context = new ApplicationDbContext(optionsBuilder.Options);
    }

    [Fact]
    public void DbConnectionExists()
    {
        Assert.True(_context.Database.CanConnect());

    }

    [Fact]
    public void DbIsRelational()
    {
        Assert.True(_context.Database.IsRelational());
    }

    [Fact]
    public void DbIsNotInMemory()
    {
        Assert.False(_context.Database.IsInMemory());
    }

    [Fact]
    public void DbIsSqlServer()
    {
        Assert.True(_context.Database.IsSqlServer());
    }

}