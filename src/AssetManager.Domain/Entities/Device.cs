namespace AssetManager.Domain.Entities;

using AssetManager.Domain.Enums;

public class Device
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

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
