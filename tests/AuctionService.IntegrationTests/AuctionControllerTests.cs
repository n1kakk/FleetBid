﻿
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace AuctionService.IntegrationTests;

public class AuctionControllerTests : IClassFixture<CustomWebAppFactory>, IAsyncLifetime    
{
    private readonly CustomWebAppFactory _customWebAppFactory;
    private readonly HttpClient _httpClient;
    private const string Gt_Id = "afbee524-5972-4075-8800-7d1f9d7b0a0c";
    public AuctionControllerTests(CustomWebAppFactory webAppFactory)
    {
        _customWebAppFactory = webAppFactory;
        _httpClient = webAppFactory.CreateClient();
    }

    [Fact]
    public async Task GetAuctionById_WithValidId_ShouldReturnAuction()
    {
        //arrange

        //act
        var response = await _httpClient.GetFromJsonAsync<AuctionDto>($"api/auctions/{Gt_Id}");

        //assert
        Assert.Equal("GT", response.Model);
    }


    [Fact]
    public async Task GetAuctions_ShouldReturn3Auctions()
    {
        //arrange


        //act
        var response = await _httpClient.GetFromJsonAsync<List<AuctionDto>>("api/auctions");

        //assert
        Assert.Equal(3, response.Count());
    }

    [Fact]
    public async Task GetAuctionById_WithInValidId_ShouldReturn404Response()
    {
        //arrange


        //act
        var response = await _httpClient.GetAsync($"api/auctions/{Guid.NewGuid()}");

        //assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAuctionById_WithInValidGuid_ShouldReturn400()
    {
        //arrange


        //act
        var response = await _httpClient.GetAsync($"api/auctions/notaguid");

        //assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task CreateAuction_WithNoAuth_ShouldReturn401()
    {
        //arrange
        var auction = new CreateAuctionDto { Make = "test" };

        //act
        var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

        //assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }


    [Fact]
    public async Task CreateAuction_WithAuth_ShouldReturn201()
    {
        //arrange
        var auction = GetAuctionForCreate();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

        //act
        var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

        //assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAuction = await response.Content.ReadFromJsonAsync<AuctionDto>();
        Assert.Equal("bob", createdAuction.Seller);
    }


    private CreateAuctionDto GetAuctionForCreate()
    {
        return new CreateAuctionDto
        {
            Make = "test",
            Model = "testModel",
            ImageUrl = "test",
            Color = "test",
            Mileage = 10,
            Year = 10,
            ReservePrice = 10
        }; 
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        using var scope = _customWebAppFactory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
        DbHelper.ReInitDbForTests(db);
        return Task.CompletedTask;
    }
}
