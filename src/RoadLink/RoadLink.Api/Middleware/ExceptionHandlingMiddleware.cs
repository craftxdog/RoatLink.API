using Microsoft.AspNetCore.Mvc;
using RoadLink.Application.Exceptions;

namespace RoatLink.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ocurred unexpected exception: {Message}", e.Message);
            var exceptionDetails = GetExceptionDetails(e);
            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Details,
            };
            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    
    private static ExceptionDetails GetExceptionDetails(Exception e)
    {
        return e switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Error Validation",
                "Se han generado uno o mas errores de validacion.",
                validationException.Errors
                ),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server Error",
                "Se ha generado un error inesperado en el servidor.",
                null
                ),
        };
    }
    
    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Details,
        IEnumerable<object>? Errors
        );
}