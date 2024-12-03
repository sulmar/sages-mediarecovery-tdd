using WebApp.Abstractions;
using WebApp.Domain.Models;

namespace WebApp.Infrastructure;

public class DbDeviceRepository : IDeviceRepository
{
    private Dictionary<int, Device> _devices = [];

    public DbDeviceRepository(IEnumerable<Device> devices)
    {
        _devices = devices.ToDictionary(p => p.Id);
    }

    public Task<IEnumerable<Device>> GetAllAsync()
    {
        return Task.FromResult(_devices.Values.AsEnumerable());
    }

    public Task<Device?> GetByIdAsync(int id)
    {
        if (_devices.TryGetValue(id, out var device))
            return Task.FromResult(device);

        return Task.FromResult<Device?>(null);
    }
}