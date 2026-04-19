using SewControl.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Dtos.Encargos;

public class EncargoDto
{
    public int Id { get; set; }
    public TipoEncargo Tipo { get; set; }
    public EstadoEncargo Estado { get; set; }
    public DateTime FechaRecepcion { get; set; }
    public DateTime FechaEntregaEstimada { get; set; }
    public DateTime? FechaEntregaReal { get; set; }
    public decimal PrecioTotal { get; set; }
    public decimal? Anticipo { get; set; }
    public string? Observaciones { get; set; }

    public int ClienteId { get; set; }
    public string NombreCliente { get; set; } = string.Empty;

    public int CostureraId { get; set; }
    public string NombreCosturera { get; set; } = string.Empty;

    public List<PrendaDto> Prendas { get; set; } = new();
    public List<ArregloDto> Arreglos { get; set; } = new();
}

public class CreateEncargoDto
{
    public TipoEncargo Tipo { get; set; }
    public DateTime FechaEntregaEstimada { get; set; }
    public decimal PrecioTotal { get; set; }
    public decimal? Anticipo { get; set; }
    public string? Observaciones { get; set; }
    public int ClienteId { get; set; }
    public int CostureraId { get; set; }
}

public class UpdateEncargoDto
{
    public EstadoEncargo Estado { get; set; }
    public DateTime? FechaEntregaEstimada { get; set; }
    public DateTime? FechaEntregaReal { get; set; }
    public decimal? PrecioTotal { get; set; }
    public string? Observaciones { get; set; }
    public int? CostureraId { get; set; }
}