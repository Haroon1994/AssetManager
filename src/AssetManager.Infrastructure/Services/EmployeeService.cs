namespace AssetManager.Infrastructure.Services;

using AssetManager.Application.DTOs;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        return await _context.Employees
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Department = e.Department
            })
            .ToListAsync();
    }

    public async Task<EmployeeDto?> GetByIdAsync(Guid id)
    {
        var e = await _context.Employees.FindAsync(id);
        if (e is null) return null;

        return new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Department = e.Department
        };
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Department = dto.Department
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Department = employee.Department
        };
    }

    public async Task<EmployeeDto?> UpdateAsync(Guid id, CreateEmployeeDto dto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null) return null;

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
        employee.Department = dto.Department;

        await _context.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Department = employee.Department
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}
