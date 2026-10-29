namespace AssetManager.Application.Interfaces;

using AssetManager.Application.DTOs;

public interface IDeviceService
{
    Task<IEnumerable<DeviceDto>> GetAllAsync();
    Task<DeviceDto?> GetByIdAsync(Guid id);
    Task<DeviceDto> CreateAsync(CreateDeviceDto dto);
    Task<DeviceDto?> UpdateAsync(Guid id, CreateDeviceDto dto);
    Task<bool> DeleteAsync(Guid id);
}
