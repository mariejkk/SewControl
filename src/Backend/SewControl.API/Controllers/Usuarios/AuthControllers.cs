using Microsoft.AspNetCore.Mvc;
using SewControl.Application.Dtos.Auth;
using SewControl.Application.Services;

namespace SewControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    public AuthController(AuthService service) => _service = service;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto) =>
        Ok(await _service.LoginAsync(dto));

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto) =>
        Ok(await _service.RegisterAsync(dto));
}