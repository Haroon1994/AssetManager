namespace AssetManager.Infrastructure.Services;

using AssetManager.Application.DTOs;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Enums;
using AssetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class AssignmentService : IAssignmentService
{
    private readonly AppDbContext _context;

    public AssignmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AssignmentDto>> GetAllAsync()
    {
        return await _context.Assignments
            .Include(a => a.Device)
            .Include(a => a.Employee)
            .Select(a => new AssignmentDto
            {
                Id = a.Id,
                DeviceId = a.DeviceId,
                DeviceName = a.Device.Name,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
                AssignedAt = a.AssignedAt,
                ReturnedAt = a.ReturnedAt,
                Notes = a.Notes
            })
            .ToListAsync();
    }

    public async Task<AssignmentDto?> GetByIdAsync(Guid id)
    {
        var a = await _context.Assignments
            .Include(a => a.Device)
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (a is null) return null;

        return new AssignmentDto
        {
            Id = a.Id,
            DeviceId = a.DeviceId,
            DeviceName = a.Device.Name,
            EmployeeId = a.EmployeeId,
            EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
            AssignedAt = a.AssignedAt,
            ReturnedAt = a.ReturnedAt,
            Notes = a.Notes
        };
    }

    public async Task<AssignmentDto> CreateAsync(CreateAssignmentDto dto)
    {
        var device = await _context.Devices.FindAsync(dto.DeviceId)
            ?? throw new ArgumentException("Device not found.");
        var employee = await _context.Employees.FindAsync(dto.EmployeeId)
            ?? throw new ArgumentException("Employee not found.");

        device.Status = DeviceStatus.Assigned;

        var assignment = new Assignment
        {
            Id = Guid.NewGuid(),
            DeviceId = dto.DeviceId,
            EmployeeId = dto.EmployeeId,
            AssignedAt = DateTime.UtcNow,
            Notes = dto.Notes
        };

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        return new AssignmentDto
        {
            Id = assignment.Id,
            DeviceId = assignment.DeviceId,
            DeviceName = device.Name,
            EmployeeId = assignment.EmployeeId,
            EmployeeName = employee.FirstName + " " + employee.LastName,
            AssignedAt = assignment.AssignedAt,
            ReturnedAt = assignment.ReturnedAt,
            Notes = assignment.Notes
        };
    }

    public async Task<AssignmentDto?> ReturnDeviceAsync(Guid assignmentId)
    {
        var a = await _context.Assignments
            .Include(a => a.Device)
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == assignmentId);

        if (a is null) return null;

        a.ReturnedAt = DateTime.UtcNow;
        a.Device.Status = DeviceStatus.Available;

        await _context.SaveChangesAsync();

        return new AssignmentDto
        {
            Id = a.Id,
            DeviceId = a.DeviceId,
            DeviceName = a.Device.Name,
            EmployeeId = a.EmployeeId,
            EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
            AssignedAt = a.AssignedAt,
            ReturnedAt = a.ReturnedAt,
            Notes = a.Notes
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment is null) return false;

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return true;
    }
}
