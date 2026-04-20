using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Application.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; }
    public AppException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}

public class NotFoundException : AppException
{
    public NotFoundException(string entidad, int id)
        : base($"{entidad} con Id {id} no fue encontrado.", 404) { }
}

public class ValidationException : AppException
{
    public List<string> Errors { get; }
    public ValidationException(List<string> errors)
        : base("Errores de validación.", 422)
    {
        Errors = errors;
    }
}
