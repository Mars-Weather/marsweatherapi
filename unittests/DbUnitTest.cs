using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web.Http.Cors;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;

namespace unittests;

public class DbUnitTest
{
    private ApplicationDbContext _context;
    private ILogger<DbUnitTest> _logger;

    public DbUnitTest(ApplicationDbContext context, ILogger<DbUnitTest> logger)
    {
        _context = context;
        _logger = logger;
    }

    [Fact]
    public void Test1()
    {
        Assert.True(1 == 1, "Test1 failed");
    }

    [Fact]
    public void Test2()
    {
        Assert.False(1 == 2, "Test2 failed");
    }

    [Fact]
    public void DbConnectionExists()
    {
        _context.Database.

    }

    [Fact]
    public void DbIsRelational()
    {

    }

    [Fact]
    public void DbIsNotInMemory()
    {

    }

    [Fact]
    public void DbIsSqlServer()
    {

    }

}