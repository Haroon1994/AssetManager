namespace AssetManager.Application.DTOs;

public class AssignmentDto
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; } = string.Empty;
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public string? Notes { get; set; }
}

public class CreateAssignmentDto
{
    public Guid DeviceId { get; set; }
    public Guid EmployeeId { get; set; }
    public string? Notes { get; set; }
}
