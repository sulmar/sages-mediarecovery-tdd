using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using WebApp.Abstractions;
using WebApp.Domain.Models;

namespace WebApp.IntegrationTests;

public class WebAppApplicationFactory : WebApplicationFactory<Program>
{
    private Mock<IDeviceRepository> mock;
    
    public WebAppApplicationFactory()
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
                new Device(),
            });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                services.AddTransient<IDeviceRepository>(dr => mock.Object);
            })
            .UseEnvironment("Development")
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {
                        "DefaultConnection", "Test"
                    }
                }!);
            });
    }
}