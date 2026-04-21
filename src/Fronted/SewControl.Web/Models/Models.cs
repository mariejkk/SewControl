namespace SewControl.Web.Models;

public enum TipoEncargo { Confeccion, Arreglo, Bordado, Otro }
public enum EstadoEncargo { Pendiente, EnProceso, Listo, Entregado, Cancelado }
public enum TipoPrenda { Camisa, Pantalon, Vestido, Falda, Chaqueta, Traje, Blusa, Otro }
public enum TipoArreglo { Dobladillo, Cremallera, Zurcido, Ajuste, Otro }

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}

public class ClienteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int TotalEncargos { get; set; }
    public string NombreCompleto => $"{Nombre} {Apellido}";
}

public class CreateClienteDto
{
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string? Email { get; set; }
    public string? Direccion { get; set; }
}

public class CostureraDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string? Especialidad { get; set; }
    public bool Activa { get; set; }
    public int EncargosActivos { get; set; }
    public string NombreCompleto => $"{Nombre} {Apellido}";
}

public class CreateCostureraDto
{
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string? Especialidad { get; set; }
}

public class PrendaDto
{
    public int Id { get; set; }
    public TipoPrenda Tipo { get; set; }
    public string Descripcion { get; set; } = "";
    public string? Tela { get; set; }
    public string? Color { get; set; }
    public string? Talla { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}

public class CreatePrendaDto
{
    public TipoPrenda Tipo { get; set; }
    public string Descripcion { get; set; } = "";
    public string? Tela { get; set; }
    public string? Color { get; set; }
    public string? Talla { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}

public class ArregloDto
{
    public int Id { get; set; }
    public TipoArreglo Tipo { get; set; }
    public string Descripcion { get; set; } = "";
    public decimal Costo { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}

public class CreateArregloDto
{
    public TipoArreglo Tipo { get; set; }
    public string Descripcion { get; set; } = "";
    public decimal Costo { get; set; }
    public string? Observaciones { get; set; }
    public int EncargoId { get; set; }
}

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
    public string NombreCliente { get; set; } = "";
    public int CostureraId { get; set; }
    public string NombreCosturera { get; set; } = "";
    public List<PrendaDto> Prendas { get; set; } = new();
    public List<ArregloDto> Arreglos { get; set; } = new();
    public decimal Saldo => PrecioTotal - (Anticipo ?? 0);
    public bool EstaAtrasado => FechaEntregaReal == null && FechaEntregaEstimada < DateTime.Today;
}

public class CreateEncargoDto
{
    public TipoEncargo Tipo { get; set; }
    public DateTime FechaEntregaEstimada { get; set; } = DateTime.Today.AddDays(7);
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
