using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Encargos;
using SewControl.Application.Services;
using SewControl.Domain.Entities.Encargos;

namespace SewControl.API.Controllers.Encargos;

/// <summary>
/// Gestiona los encargos de costura: creación, consulta, actualización y sus prendas/arreglos asociados.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EncargosController : ControllerBase
{
    private readonly EncargoService _service;

    public EncargosController(EncargoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los encargos registrados.
    /// </summary>
    /// <response code="200">Lista de encargos obtenida correctamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    /// <summary>
    /// Obtiene un encargo específico por su Id.
    /// </summary>
    /// <param name="id">Id del encargo a consultar.</param>
    /// <response code="200">Encargo encontrado.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un encargo con ese Id.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetByIdAsync(id));

    /// <summary>
    /// Obtiene todos los encargos de un cliente específico.
    /// </summary>
    /// <param name="clienteId">Id del cliente.</param>
    /// <response code="200">Lista de encargos del cliente obtenida correctamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpGet("cliente/{clienteId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByCliente(int clienteId) =>
        Ok(await _service.GetByClienteAsync(clienteId));

    /// <summary>
    /// Obtiene todos los encargos asignados a una costurera específica.
    /// </summary>
    /// <param name="costureraId">Id de la costurera.</param>
    /// <response code="200">Lista de encargos de la costurera obtenida correctamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpGet("costurera/{costureraId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByCosturera(int costureraId) =>
        Ok(await _service.GetByCostureraAsync(costureraId));

    /// <summary>
    /// Obtiene todos los encargos que se encuentran en un estado específico.
    /// </summary>
    /// <param name="estado">Estado del encargo (por ejemplo: Pendiente, EnProceso, Entregado).</param>
    /// <response code="200">Lista de encargos filtrada por estado.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpGet("estado/{estado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByEstado(EstadoEncargo estado) =>
        Ok(await _service.GetByEstadoAsync(estado));

    /// <summary>
    /// Crea un nuevo encargo.
    /// </summary>
    /// <param name="dto">Datos del encargo a crear.</param>
    /// <response code="200">Encargo creado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateEncargoDto dto) =>
        Ok(await _service.CreateAsync(dto));

    /// <summary>
    /// Actualiza el estado de un encargo existente.
    /// </summary>
    /// <param name="id">Id del encargo.</param>
    /// <param name="dto">Nuevo estado del encargo.</param>
    /// <response code="200">Estado del encargo actualizado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un encargo con ese Id.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEncargoDto dto) =>
        Ok(await _service.UpdateEstadoAsync(id, dto));

    /// <summary>
    /// Edita los datos completos de un encargo existente.
    /// </summary>
    /// <param name="id">Id del encargo.</param>
    /// <param name="dto">Datos actualizados del encargo.</param>
    /// <response code="200">Encargo editado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un encargo con ese Id.</response>
    [HttpPut("editar/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEncargo(int id, [FromBody] CreateEncargoDto dto) =>
        Ok(await _service.UpdateEncargoAsync(id, dto));

    /// <summary>
    /// Elimina un encargo existente.
    /// </summary>
    /// <param name="id">Id del encargo a eliminar.</param>
    /// <response code="200">Encargo eliminado exitosamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un encargo con ese Id.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteAsync(id));

    /// <summary>
    /// Agrega una prenda a un encargo existente.
    /// </summary>
    /// <param name="dto">Datos de la prenda a agregar.</param>
    /// <response code="200">Prenda agregada exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpPost("prendas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddPrenda([FromBody] CreatePrendaDto dto) =>
        Ok(await _service.AddPrendaAsync(dto));

    /// <summary>
    /// Elimina una prenda de un encargo.
    /// </summary>
    /// <param name="prendaId">Id de la prenda a eliminar.</param>
    /// <response code="200">Prenda eliminada exitosamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe una prenda con ese Id.</response>
    [HttpDelete("prendas/{prendaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePrenda(int prendaId) =>
        Ok(await _service.DeletePrendaAsync(prendaId));

    /// <summary>
    /// Agrega un arreglo a un encargo existente.
    /// </summary>
    /// <param name="dto">Datos del arreglo a agregar.</param>
    /// <response code="200">Arreglo agregado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpPost("arreglos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddArreglo([FromBody] CreateArregloDto dto) =>
        Ok(await _service.AddArregloAsync(dto));

    /// <summary>
    /// Elimina un arreglo de un encargo.
    /// </summary>
    /// <param name="arregloId">Id del arreglo a eliminar.</param>
    /// <response code="200">Arreglo eliminado exitosamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un arreglo con ese Id.</response>
    [HttpDelete("arreglos/{arregloId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArreglo(int arregloId) =>
        Ok(await _service.DeleteArregloAsync(arregloId));
}