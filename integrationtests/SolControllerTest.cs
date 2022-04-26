using Xunit;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using MarsWeatherApi.Models;
using MarsWeatherApi.Controllers;

namespace integrationtests;

public class SolControllerTest
{
    //static readonly HttpClient client = new HttpClient();

    private readonly HttpClient client;

    private ILogger<SolControllerTest> _logger;

    /*static SolControllerTest()
    {
        client = new HttpClient();
    }*/

    public SolControllerTest()
    {
        client = new HttpClient();
        //_logger = new ILogger<SolControllerTest>();
        //_logger = logger;
        //ILogger<SolControllerTest> logi = 
        //ILoggerFactory facto = new ILoggerFactory();
        //_logger = factory.CreateLogger<SolControllerTest>();

        var loggerFactory = (ILoggerFactory)new LoggerFactory();
        _logger = loggerFactory.CreateLogger<SolControllerTest>();

        
    }

    [Fact]
    public async void CorrectSolPostReturns201() // KESKEN
    {   
        try
        {
            // Arrange
            var sol = new Sol()
            {
                Wind = {
                    Average = 530,
                    Minimum = 2220,
                    Maximum = 8740,
                    MostCommonDirection = "SW"
                },
                Temperature = {
                    Average = 130,
                    Minimum = 5120,
                    Maximum = 240
                },
                Pressure = {
                    Average = 50,
                    Minimum = 50,
                    Maximum = 50
                },
                Start = new DateTime(2035, 12, 01, 08, 43, 34),
                End = new DateTime(2035, 02, 13, 09, 23, 09),
                Season = "Winter",
                SolNumber = 1
            };

            // Act
            var response = await client.PostAsJsonAsync("https://marsweather.azurewebsites.net/api/sol", sol);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(response.StatusCode.Equals("201"), "Status code was " + response.StatusCode);
        }
        catch(HttpRequestException e)
        {
            _logger.LogError(e.Message);
        }

    }

    /*[Fact]
    public async void IncorrectSolPostReturns400() // KESKEN
    {
        try
        {
            // Arrange
            var sol = @"{
    "Wind": {
        "Average": "530.6",
        "Minimum": "2220.3",
        "Maximum": "8740.9",
        "mostCommonDirection": "SW"
    },
    "Temperature": {
        "Average": 130.6,
        "Minimum": 5120.3,
        "Maximum": 240.9
    },
    "Pressure": {
        "Average": 50.6,
        "Minimum": 50.3,
        "Maximum": 50.9
    },
    "Start": "2035-02-12T08:43:34Z",
    "End": "2035-02-13T09:23:09Z",
    "Season": "Winter",
    "SolNumber": 1
}";

            // Act
            var response = await client.PostAsJsonAsync("https://marsweather.azurewebsites.net/api/sol", sol);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(response.StatusCode.Equals("201"), "Status code was " + response.StatusCode);
        }
        catch(HttpRequestException e)
        {
            _logger.LogError(e.Message);
        }
    }*/

    [Fact]
    public async void DbIsNotEmpty()
    {
        try
        {
            // Arrange
            HttpResponseMessage response = await client.GetAsync("https://marsweather.azurewebsites.net/api/sol");
            response.EnsureSuccessStatusCode();

            // Act
            string jsonString = await response.Content.ReadAsStringAsync();
            JsonNode solsNode = JsonNode.Parse(jsonString)!;
            JsonArray solList = solsNode!["$values"]!.AsArray()!;

            // Assert that the returned list is not empty
            Assert.True(solList.Count != 0, "The list was empty");
        }   
        catch(HttpRequestException e)
        {
            _logger.LogError(e.Message);
        }  
    }

    [Fact]
    public async void LastSevenSolsContainsSeven()
    {   
        try
        {
            // Arrange
            HttpResponseMessage response = await client.GetAsync("https://marsweather.azurewebsites.net/api/sol/solweek");
            response.EnsureSuccessStatusCode();

            // Act
            string jsonString = await response.Content.ReadAsStringAsync();
            JsonNode solsNode = JsonNode.Parse(jsonString)!;
            JsonArray solList = solsNode!.AsArray()!;

            // Assert that the returned list contains 7 items
            Assert.True(solList.Count == 7, "The list did not contain 7 items");
        }   
        catch(HttpRequestException e)
        {
            _logger.LogError(e.Message);
        }  
    }

}