namespace AssetManager.API.Controllers;

using AssetManager.Application.DTOs;
using AssetManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentService _service;

    public AssignmentsController(IAssignmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAll()
    {
        var assignments = await _service.GetAllAsync();
        return Ok(assignments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentDto>> GetById(Guid id)
    {
        var assignment = await _service.GetByIdAsync(id);
        if (assignment is null) return NotFound();
        return Ok(assignment);
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentDto>> Create(CreateAssignmentDto dto)
    {
        var assignment = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = assignment.Id }, assignment);
    }

    [HttpPatch("{id}/return")]
    public async Task<ActionResult<AssignmentDto>> ReturnDevice(Guid id)
    {
        var assignment = await _service.ReturnDeviceAsync(id);
        if (assignment is null) return NotFound();
        return Ok(assignment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
