using SewControl.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Domain.Entities.Usuarios;
public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    public ICollection<Encargos.Encargo> Encargos { get; set; } = new List<Encargos.Encargo>();
}
