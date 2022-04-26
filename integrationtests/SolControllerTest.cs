using Xunit;
using System;
using System.Threading.Tasks;
using System.Text.Json;
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
    private readonly HttpClient _client;

    private ILogger<SolControllerTest> _logger;

    public SolControllerTest()
    {
        _client = new HttpClient();
        var loggerFactory = (ILoggerFactory)new LoggerFactory();
        _logger = loggerFactory.CreateLogger<SolControllerTest>();       
    }

    [Fact]
    public async void DbIsNotEmpty()
    {
        try
        {
            // Arrange
            HttpResponseMessage response = await _client.GetAsync("https://marsweather.azurewebsites.net/api/sol");
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
            HttpResponseMessage response = await _client.GetAsync("https://marsweather.azurewebsites.net/api/sol/solweek");
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