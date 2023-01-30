using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Application.Integration.Tests.Helpers;

namespace Tests.Application.Integration.Tests.Controllers;
public class ViechleControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly Mock<IIdentityService> _identityService;
    public ViechleControllerTests(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        _identityService = new Mock<IIdentityService>();
    }
    [Fact]
    public async void Get_Returns401Unauthorized_ifNotAuthenticated(){
        //Arrange
        //Act
        var response = await _httpClient.GetAsync("api/Viechle");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Get_ReturnsAllViechles_ifAuthenticated()
    {
        //Arrange

        await  _httpClient.Authenticate();
        // _httpClient.DefaultRequestHeaders.Authorization = new  AuthenticationHeaderValue("Test");
        
        //Act
        var response = await _httpClient.GetAsync("/api/Viechle");

        //Assert
        response.EnsureSuccessStatusCode();
        var veichles = JArray.Parse(await response.Content.ReadAsStringAsync());
        veichles.Count.Should().Be(3);
        response.StatusCode.Should().Be(HttpStatusCode.OK);


    }
    [Fact]
    public async void Delete_RemovesSingleViechle(){
        //Arrange
        await _httpClient.Authenticate();
        var res1 = await _httpClient.GetAsync("/api/Viechle");
        var expectedCount = JArray.Parse(await res1.Content.ReadAsStringAsync()).Count -1;
        //Act
        var response = await _httpClient.DeleteAsync("/api/Viechle/1");
        var res2 = await _httpClient.GetAsync("/api/Viechle");
        var actualCount = JArray.Parse(await res2.Content.ReadAsStringAsync()).Count;
        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        actualCount.Should().Be(expectedCount);

    }
    [Fact]
     public async void Delete_Returns404NotFound_forUnknownId(){
        //Arrange
        await _httpClient.Authenticate();
        //Act
        var response = await _httpClient.DeleteAsync("/api/Viechle/19");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        

    }
}