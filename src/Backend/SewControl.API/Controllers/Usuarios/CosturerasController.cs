using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Usuarios;
using SewControl.Application.Services;

namespace SewControl.API.Controllers.Usuarios;

/// <summary>
/// Gestiona el registro, consulta, actualización y estado de las costureras.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CosturerasController : ControllerBase
{
    private readonly UsuariosService _service;

    public CosturerasController(UsuariosService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todas las costureras registradas.
    /// </summary>
    /// <response code="200">Lista de costureras obtenida correctamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllCosturerasAsync());

    /// <summary>
    /// Obtiene una costurera específica por su Id.
    /// </summary>
    /// <param name="id">Id de la costurera a consultar.</param>
    /// <response code="200">Costurera encontrada.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe una costurera con ese Id.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetCostureraByIdAsync(id));

    /// <summary>
    /// Actualiza los datos de una costurera existente.
    /// </summary>
    /// <param name="id">Id de la costurera a actualizar.</param>
    /// <param name="dto">Nuevos datos de la costurera.</param>
    /// <response code="200">Costurera actualizada exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe una costurera con ese Id.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCostureraDto dto) =>
        Ok(await _service.UpdateCostureraAsync(id, dto));

    /// <summary>
    /// Registra una nueva costurera.
    /// </summary>
    /// <param name="dto">Datos de la costurera a crear.</param>
    /// <response code="200">Costurera creada exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateCostureraDto dto) =>
        Ok(await _service.CreateCostureraAsync(dto));

    /// <summary>
    /// Activa o desactiva a una costurera (cambia su estado actual al opuesto).
    /// </summary>
    /// <param name="id">Id de la costurera.</param>
    /// <response code="200">Estado de la costurera actualizado exitosamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe una costurera con ese Id.</response>
    [HttpPatch("{id}/toggle-activa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleActiva(int id) =>
        Ok(await _service.ToggleActivaAsync(id));

    /// <summary>
    /// Elimina una costurera existente.
    /// </summary>
    /// <param name="id">Id de la costurera a eliminar.</param>
    /// <response code="200">Costurera eliminada exitosamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe una costurera con ese Id.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteCostureraAsync(id));
}