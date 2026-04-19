using SewControl.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Domain.Entities.Usuarios;
public class Costurera
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
    public bool Activa { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public ICollection<Encargo> Encargos { get; set; } = new List<Encargo>();
}
