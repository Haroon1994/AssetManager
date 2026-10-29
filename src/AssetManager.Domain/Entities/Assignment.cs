namespace AssetManager.Domain.Entities;

public class Assignment
{
    public Guid Id { get; set; }

    public Guid DeviceId { get; set; }
    public Device Device { get; set; } = null!;

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public DateTime AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public string? Notes { get; set; }
}
