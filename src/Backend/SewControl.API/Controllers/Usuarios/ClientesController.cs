using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Usuarios;
using SewControl.Application.Services;

namespace SewControl.API.Controllers.Usuarios;

/// <summary>
/// Gestiona el registro, consulta, actualización y eliminación de clientes.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly UsuariosService _service;

    public ClientesController(UsuariosService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los clientes registrados.
    /// </summary>
    /// <response code="200">Lista de clientes obtenida correctamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllClientesAsync());

    /// <summary>
    /// Obtiene un cliente específico por su Id.
    /// </summary>
    /// <param name="id">Id del cliente a consultar.</param>
    /// <response code="200">Cliente encontrado.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un cliente con ese Id.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetClienteByIdAsync(id));

    /// <summary>
    /// Registra un nuevo cliente.
    /// </summary>
    /// <param name="dto">Datos del cliente a crear.</param>
    /// <response code="200">Cliente creado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto dto) =>
        Ok(await _service.CreateClienteAsync(dto));

    /// <summary>
    /// Actualiza los datos de un cliente existente.
    /// </summary>
    /// <param name="id">Id del cliente a actualizar.</param>
    /// <param name="dto">Nuevos datos del cliente.</param>
    /// <response code="200">Cliente actualizado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un cliente con ese Id.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateClienteDto dto) =>
        Ok(await _service.UpdateClienteAsync(id, dto));

    /// <summary>
    /// Elimina un cliente existente.
    /// </summary>
    /// <param name="id">Id del cliente a eliminar.</param>
    /// <response code="200">Cliente eliminado exitosamente.</response>
    /// <response code="401">No estás registrado o el token es inválido.</response>
    /// <response code="404">No existe un cliente con ese Id.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteClienteAsync(id));
}