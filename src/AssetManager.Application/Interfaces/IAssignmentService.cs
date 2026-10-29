namespace AssetManager.Application.Interfaces;

using AssetManager.Application.DTOs;

public interface IAssignmentService
{
    Task<IEnumerable<AssignmentDto>> GetAllAsync();
    Task<AssignmentDto?> GetByIdAsync(Guid id);
    Task<AssignmentDto> CreateAsync(CreateAssignmentDto dto);
    Task<AssignmentDto?> ReturnDeviceAsync(Guid assignmentId);
    Task<bool> DeleteAsync(Guid id);
}
