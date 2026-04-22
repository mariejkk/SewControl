using SewControl.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Dtos.Encargos;
public class ArregloDto
{
    public int Id { get; set; }
    public TipoArreglo Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}

public class CreateArregloDto
{
    public TipoArreglo Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}