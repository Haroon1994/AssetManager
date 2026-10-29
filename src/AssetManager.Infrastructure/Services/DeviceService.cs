namespace AssetManager.Infrastructure.Services;

using AssetManager.Application.DTOs;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class DeviceService : IDeviceService
{
    private readonly AppDbContext _context;

    public DeviceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeviceDto>> GetAllAsync()
    {
        return await _context.Devices
            .Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                SerialNumber = d.SerialNumber,
                Category = d.Category,
                Status = d.Status,
                PurchaseDate = d.PurchaseDate,
                WarrantyExpiryDate = d.WarrantyExpiryDate,
                PurchasePrice = d.PurchasePrice,
                Notes = d.Notes
            })
            .ToListAsync();
    }

    public async Task<DeviceDto?> GetByIdAsync(Guid id)
    {
        var d = await _context.Devices.FindAsync(id);
        if (d is null) return null;

        return new DeviceDto
        {
            Id = d.Id,
            Name = d.Name,
            SerialNumber = d.SerialNumber,
            Category = d.Category,
            Status = d.Status,
            PurchaseDate = d.PurchaseDate,
            WarrantyExpiryDate = d.WarrantyExpiryDate,
            PurchasePrice = d.PurchasePrice,
            Notes = d.Notes
        };
    }

    public async Task<DeviceDto> CreateAsync(CreateDeviceDto dto)
    {
        var device = new Device
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Category = dto.Category,
            Status = dto.Status,
            PurchaseDate = dto.PurchaseDate,
            WarrantyExpiryDate = dto.WarrantyExpiryDate,
            PurchasePrice = dto.PurchasePrice,
            Notes = dto.Notes
        };

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return new DeviceDto
        {
            Id = device.Id,
            Name = device.Name,
            SerialNumber = device.SerialNumber,
            Category = device.Category,
            Status = device.Status,
            PurchaseDate = device.PurchaseDate,
            WarrantyExpiryDate = device.WarrantyExpiryDate,
            PurchasePrice = device.PurchasePrice,
            Notes = device.Notes
        };
    }

    public async Task<DeviceDto?> UpdateAsync(Guid id, CreateDeviceDto dto)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null) return null;

        device.Name = dto.Name;
        device.SerialNumber = dto.SerialNumber;
        device.Category = dto.Category;
        device.Status = dto.Status;
        device.PurchaseDate = dto.PurchaseDate;
        device.WarrantyExpiryDate = dto.WarrantyExpiryDate;
        device.PurchasePrice = dto.PurchasePrice;
        device.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return new DeviceDto
        {
            Id = device.Id,
            Name = device.Name,
            SerialNumber = device.SerialNumber,
            Category = device.Category,
            Status = device.Status,
            PurchaseDate = device.PurchaseDate,
            WarrantyExpiryDate = device.WarrantyExpiryDate,
            PurchasePrice = device.PurchasePrice,
            Notes = device.Notes
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null) return false;

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
        return true;
    }
}
