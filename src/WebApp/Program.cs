using WebApp.Abstractions;
using WebApp.Domain.Models;
using WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDeviceRepository, DbDeviceRepository>();
builder.Services.AddTransient<IEnumerable<Device>>(s =>
{
    var devices = new List<Device>
    {
        new Device { Id = 1, Name = "device from db 1"},
        new Device { Id = 2, Name = "device from db 2"},
        new Device { Id = 3, Name = "device from db 3"},
    };

    return devices;
});

    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// public partial class Program { }