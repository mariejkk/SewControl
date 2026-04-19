using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Domain.Entities.Encargos;
public enum EstadoEncargo
{
    Pendiente,
    EnProceso,
    Listo,
    Entregado,
    Cancelado
}

public enum TipoEncargo
{
    Confeccion,
    Arreglo,
    Bordado,
    Otro
}

public class Encargo
{
    public int Id { get; set; }
    public TipoEncargo Tipo { get; set; }
    public EstadoEncargo Estado { get; set; } = EstadoEncargo.Pendiente;

    public DateTime FechaRecepcion { get; set; } = DateTime.UtcNow;
    public DateTime FechaEntregaEstimada { get; set; }
    public DateTime? FechaEntregaReal { get; set; }

    public decimal PrecioTotal { get; set; }
    public decimal? Anticipo { get; set; }
    public string? Observaciones { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int ClienteId { get; set; }
    public Usuarios.Cliente Cliente { get; set; } = null!;

    public int CostureraId { get; set; }
    public Usuarios.Costurera Costurera { get; set; } = null!;

    public ICollection<Prenda> Prendas { get; set; } = new List<Prenda>();
    public ICollection<Arreglo> Arreglos { get; set; } = new List<Arreglo>();
}