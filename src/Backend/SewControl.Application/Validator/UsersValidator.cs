using SewControl.Application.Dtos.Usuarios;
using SewControl.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ClienteValidator
{
    public static void Validate(CreateClienteDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            errors.Add("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Apellido))
            errors.Add("El apellido es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Telefono))
            errors.Add("El teléfono es obligatorio.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}

public static class CostureraValidator
{
    public static void Validate(CreateCostureraDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            errors.Add("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Apellido))
            errors.Add("El apellido es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Telefono))
            errors.Add("El teléfono es obligatorio.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}