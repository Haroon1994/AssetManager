namespace AssetManager.API.Controllers;

using AssetManager.Application.DTOs;
using AssetManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _service;

    public DevicesController(IDeviceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAll()
    {
        var devices = await _service.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DeviceDto>> GetById(Guid id)
    {
        var device = await _service.GetByIdAsync(id);
        if (device is null) return NotFound();
        return Ok(device);
    }

    [HttpPost]
    public async Task<ActionResult<DeviceDto>> Create(CreateDeviceDto dto)
    {
        var device = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DeviceDto>> Update(Guid id, CreateDeviceDto dto)
    {
        var device = await _service.UpdateAsync(id, dto);
        if (device is null) return NotFound();
        return Ok(device);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
