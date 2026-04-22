using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Domain.Entities.Encargos;

public enum TipoArreglo
{
    Dobladillo,
    Cremallera,
    Zurcido,
    Ajuste,
    Otro
}

public class Arreglo
{
    public int Id { get; set; }
    public TipoArreglo Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public string? Observaciones { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int EncargoId { get; set; }
    public Encargo Encargo { get; set; } = null!;
}
