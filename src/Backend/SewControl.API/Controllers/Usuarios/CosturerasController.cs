using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Usuarios;
using SewControl.Application.Services;

namespace SewControl.API.Controllers.Usuarios;

[ApiController]
[Route("api/[controller]")]
public class CosturerasController : ControllerBase
{
    private readonly UsuariosService _service;

    public CosturerasController(UsuariosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllCosturerasAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetCostureraByIdAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCostureraDto dto) =>
    Ok(await _service.UpdateCostureraAsync(id, dto));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCostureraDto dto) =>
        Ok(await _service.CreateCostureraAsync(dto));

    [HttpPatch("{id}/toggle-activa")]
    public async Task<IActionResult> ToggleActiva(int id) =>
        Ok(await _service.ToggleActivaAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteCostureraAsync(id));
}