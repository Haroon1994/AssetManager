namespace AssetManager.Application.Interfaces;

using AssetManager.Application.DTOs;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(Guid id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
    Task<EmployeeDto?> UpdateAsync(Guid id, CreateEmployeeDto dto);
    Task<bool> DeleteAsync(Guid id);
}
