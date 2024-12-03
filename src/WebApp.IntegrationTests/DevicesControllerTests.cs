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

public class DevicesControllerTests
{
    [Fact]
    public async Task Get_Index_ShouldReturnOkWithContentHelloWorld()
    {
        // Arrange
        Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
        mock.Setup(dr => dr.GetAllAsync())
            .ReturnsAsync(new List<Device>
            {
                new Device(),
                new Device(),
                new Device(),
                new Device(),
                new Device(),
            });
        
        using var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddTransient<IDeviceRepository>(dr => mock.Object);
                    });
                    
                    builder.UseEnvironment("Development");

                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddInMemoryCollection(new Dictionary<string, string>
                        {
                            {
                                "DefaultConnection", "Test"
                            }
                        }!);
                    });


                })
            ;
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/devices");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<Device>>();
        content.Should().HaveCount(5);
    }
}