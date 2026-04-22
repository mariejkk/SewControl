using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Domain.Entities.Encargos;

public enum TipoPrenda
{
    Camisa,
    Pantalon,
    Vestido,
    Falda,
    Chaqueta,
    Traje,
    Blusa,
    Otro
}

public class Prenda
{
    public int Id { get; set; }
    public TipoPrenda Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string? Tela { get; set; }
    public string? Color { get; set; }
    public string? Talla { get; set; }
    public string? Observaciones { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int EncargoId { get; set; }
    public Encargo Encargo { get; set; } = null!;
}
