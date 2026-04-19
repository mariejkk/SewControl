using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Dtos.Usuarios;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int TotalEncargos { get; set; }
}

public class CreateClienteDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Direccion { get; set; }
}
