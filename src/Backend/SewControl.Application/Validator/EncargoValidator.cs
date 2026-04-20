using SewControl.Application.Dtos.Encargos;
using SewControl.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Validators;

public static class EncargoValidator
{
    public static void Validate(CreateEncargoDto dto)
    {
        var errors = new List<string>();

        if (dto.ClienteId <= 0)
            errors.Add("Debe seleccionar un cliente.");

        if (dto.CostureraId <= 0)
            errors.Add("Debe seleccionar una costurera.");

        if (dto.PrecioTotal <= 0)
            errors.Add("El precio total debe ser mayor a 0.");

        if (dto.FechaEntregaEstimada <= DateTime.Today)
            errors.Add("La fecha de entrega debe ser posterior a hoy.");

        if (dto.Anticipo.HasValue && dto.Anticipo > dto.PrecioTotal)
            errors.Add("El anticipo no puede ser mayor al precio total.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    public static void ValidateUpdate(UpdateEncargoDto dto)
    {
        var errors = new List<string>();

        if (dto.CostureraId.HasValue && dto.CostureraId <= 0)
            errors.Add("Debe seleccionar una costurera válida.");

        if (dto.PrecioTotal.HasValue && dto.PrecioTotal <= 0)
            errors.Add("El precio total debe ser mayor a 0.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}