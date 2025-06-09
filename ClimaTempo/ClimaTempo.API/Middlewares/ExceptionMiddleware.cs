using FluentValidation;
using System.Net;
using System.Text.Json;

namespace ClimaTempo.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Erro = e.ErrorMessage
                });

                var response = JsonSerializer.Serialize(new { erros = errors });
                await context.Response.WriteAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno no servidor.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { erro = "Erro interno no servidor." }));
            }
        }
    }
}
