using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SewControl.Application.Dtos.Auth;
using SewControl.Application.Exceptions;
using SewControl.Application.Responses;
using SewControl.Domain.Entities;
using SewControl.Infrastructure.Repositories;

namespace SewControl.Application.Services;

public class AuthService
{
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _config;

    public AuthService(IUnitOfWork uow, IConfiguration config)
    {
        _uow = uow;
        _config = config;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto)
    {
        var usuario = await _uow.Usuarios.GetByEmailAsync(dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            return ApiResponse<LoginResponseDto>.Fail("Usuario o contraseña incorrectos.");

        var token = GenerarToken(usuario);

        return ApiResponse<LoginResponseDto>.Ok(new LoginResponseDto
        {
            Token = token,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        }, "Login exitoso.");
    }

    public async Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterDto dto)
    {
        var existe = await _uow.Usuarios.GetByEmailAsync(dto.Email);
        if (existe != null)
            throw new AppException("El email ya está registrado.", 400);

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _uow.Usuarios.AddAsync(usuario);
        await _uow.SaveChangesAsync();

        var token = GenerarToken(usuario);

        return ApiResponse<LoginResponseDto>.Ok(new LoginResponseDto
        {
            Token = token,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        }, "Usuario registrado exitosamente.");
    }

    private string GenerarToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nombre)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}