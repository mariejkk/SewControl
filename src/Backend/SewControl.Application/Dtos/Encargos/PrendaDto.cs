using SewControl.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Dtos.Encargos;

public class PrendaDto
{
    public int Id { get; set; }
    public TipoPrenda Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string? Tela { get; set; }
    public string? Color { get; set; }
    public string? Talla { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}

public class CreatePrendaDto
{
    public TipoPrenda Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string? Tela { get; set; }
    public string? Color { get; set; }
    public string? Talla { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}