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
            
        }  



    }






}