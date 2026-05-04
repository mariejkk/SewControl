using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Dtos.Auth;

public class LoginDto
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class LoginResponseDto
{
    public string Token { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Email { get; set; } = "";
}

public class RegisterDto
{
    public string Nombre { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}