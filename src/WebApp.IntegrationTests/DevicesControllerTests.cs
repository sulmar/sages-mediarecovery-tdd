using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApp.Abstractions;
using WebApp.Domain.Models;

namespace WebApp.IntegrationTests;


public class DevicesControllerTests : IClassFixture<WebAppApplicationFactory>
{
    private readonly WebAppApplicationFactory _factory;
    
    private Mock<IDeviceRepository> mock;
    
    public DevicesControllerTests(WebAppApplicationFactory factory)
    {
        _factory = factory;
        // _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        // {
        //     AllowAutoRedirect = false
        // });
    }
    
    [Fact]
    public async Task Get_Index_ShouldReturnOkWithContentHelloWorld()
    {
        // Arrange
        mock = new Mock<IDeviceRepository>();
         
        mock.Setup(dr => dr.GetAllAsync())
            .ReturnsAsync(new List<Device>
            {
                new Device(),
                new Device(),
                new Device(),
                new Device(),
            });
        
        var client = _factory.WithWebHostBuilder(builder => builder.ConfigureServices(
                services => services.AddTransient<IDeviceRepository>(dr => mock.Object)))
            .CreateClient();
        
        // Act
        var response = await client.GetAsync("api/devices");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<Device>>();
        content.Should().HaveCount(4);
    }
    
    [Fact]
    public async Task Get_Index_ShouldReturnOkWithContentHelloWorld2()
    {
        // Arrange
        mock = new Mock<IDeviceRepository>();
         
        mock.Setup(dr => dr.GetAllAsync())
            .ReturnsAsync(new List<Device>
            {
                new Device(),
                new Device(),
            });
    
    
        var client = _factory.WithWebHostBuilder(builder => builder.ConfigureServices(
                services => services.AddTransient<IDeviceRepository>(dr => mock.Object)))
            .CreateClient();
        
        // Act
        var response = await client.GetAsync("api/devices");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<Device>>();
        content.Should().HaveCount(2);
    }
    
    [Fact]
    public async Task Get_Index_ShouldReturnOkWithContentHelloWorld5()
    {
        // Arrange
    
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("api/devices");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<Device>>();
        content.Should().HaveCount(5);
    }
}