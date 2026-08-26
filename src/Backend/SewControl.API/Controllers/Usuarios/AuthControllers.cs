using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Auth;
using SewControl.Application.Services;

namespace SewControl.API.Controllers;

/// <summary>
/// Gestiona el registro y autenticación de usuarios. Estos endpoints son públicos: no requieren token.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    public AuthController(AuthService service) => _service = service;

    /// <summary>
    /// Inicia sesión con un usuario ya registrado y devuelve un token JWT.
    /// </summary>
    /// <param name="dto">Email y contraseña del usuario.</param>
    /// <response code="200">Login exitoso. Devuelve el token JWT junto con nombre y email del usuario.</response>
    /// <response code="400">Usuario o contraseña incorrectos.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto) =>
        Ok(await _service.LoginAsync(dto));

    /// <summary>
    /// Registra un nuevo usuario y devuelve un token JWT listo para usar.
    /// </summary>
    /// <param name="dto">Nombre, email y contraseña del nuevo usuario.</param>
    /// <response code="200">Usuario registrado exitosamente. Devuelve el token JWT junto con nombre y email.</response>
    /// <response code="400">El email ya está registrado o los datos son inválidos.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto) =>
        Ok(await _service.RegisterAsync(dto));
}