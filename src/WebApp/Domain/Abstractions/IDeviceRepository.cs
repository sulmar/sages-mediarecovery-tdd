using WebApp.Domain.Models;

namespace WebApp.Abstractions;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetAllAsync();
    Task<Device?> GetByIdAsync(int id);
}