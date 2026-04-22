using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Encargos;
using SewControl.Domain.Entities.Encargos;
using SewControl.Application.Services;

namespace SewControl.API.Controllers.Encargos;

[ApiController]
[Route("api/[controller]")]
public class EncargosController : ControllerBase
{
    private readonly EncargoService _service;

    public EncargosController(EncargoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetByIdAsync(id));

    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> GetByCliente(int clienteId) =>
        Ok(await _service.GetByClienteAsync(clienteId));

    [HttpGet("costurera/{costureraId}")]
    public async Task<IActionResult> GetByCosturera(int costureraId) =>
        Ok(await _service.GetByCostureraAsync(costureraId));

    [HttpGet("estado/{estado}")]
    public async Task<IActionResult> GetByEstado(EstadoEncargo estado) =>
        Ok(await _service.GetByEstadoAsync(estado));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEncargoDto dto) =>
        Ok(await _service.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEncargoDto dto) =>
        Ok(await _service.UpdateEstadoAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteAsync(id));

    [HttpPost("prendas")]
    public async Task<IActionResult> AddPrenda([FromBody] CreatePrendaDto dto) =>
        Ok(await _service.AddPrendaAsync(dto));

    [HttpDelete("prendas/{prendaId}")]
    public async Task<IActionResult> DeletePrenda(int prendaId) =>
        Ok(await _service.DeletePrendaAsync(prendaId));

    [HttpPost("arreglos")]
    public async Task<IActionResult> AddArreglo([FromBody] CreateArregloDto dto) =>
        Ok(await _service.AddArregloAsync(dto));

    [HttpDelete("arreglos/{arregloId}")]
    public async Task<IActionResult> DeleteArreglo(int arregloId) =>
        Ok(await _service.DeleteArregloAsync(arregloId));
}
