using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Usuarios;
using SewControl.Application.Services;

namespace SewControl.API.Controllers.Usuarios;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly UsuariosService _service;

    public ClientesController(UsuariosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllClientesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetClienteByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto dto) =>
        Ok(await _service.CreateClienteAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateClienteDto dto) =>
        Ok(await _service.UpdateClienteAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteClienteAsync(id));
}