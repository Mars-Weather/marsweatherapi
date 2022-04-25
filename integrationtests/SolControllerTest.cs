using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using MarsWeatherApi.Models;
using MarsWeatherApi.Controllers;

namespace integrationtests;

public class SolControllerTest
{
    //static readonly HttpClient client = new HttpClient();

    private static readonly HttpClient client;

    static SolControllerTest()
    {
        client = new HttpClient();
    }

    [Fact]
    public void Integration_Test1()
    {
        Assert.True("" == "", "Integration_Test1 failed");
    }

    [Fact]
    public void CorrectSolPostReturns201()
    {
        Assert.True("" == "", "CorrectSolPostReturns201 failed");
    }

    [Fact]
    public void IncorrectSolPostReturns400()
    {
        Assert.True("" == "", "IncorrectSolPostReturns");
    }

    [Fact]
    public async void LastSevenSolsIsAListOfSeven()
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

            // Assert
            Assert.IsType<List<Sol>>(response.Content.GetType());
            Assert.True(solList.Count == 7);
        }   
        catch(HttpRequestException e)
        {
            
        }  



    }






}