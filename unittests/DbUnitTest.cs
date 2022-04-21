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

    public DbUnitTest(ApplicationDbContext context)
    {
        //_context = context;
        _context = new ApplicationDbContext();
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