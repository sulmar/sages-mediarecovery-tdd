using Microsoft.AspNetCore.Mvc;
using WebApp.Abstractions;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController(IDeviceRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var devices = await repository.GetAllAsync();
        
        return Ok(devices);
    }
}