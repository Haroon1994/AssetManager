namespace AssetManager.Application.DTOs;

using AssetManager.Domain.Enums;

public class DeviceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public DeviceCategory Category { get; set; }
    public DeviceStatus Status { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public string? Notes { get; set; }
}

public class CreateDeviceDto
{
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public DeviceCategory Category { get; set; }
    public DeviceStatus Status { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public string? Notes { get; set; }
}
