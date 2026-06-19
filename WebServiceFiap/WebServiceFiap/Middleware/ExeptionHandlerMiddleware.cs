using System.Net;
using System.Text.Json;
 
namespace WebServiceFiap.Middleware
{
    /// <summary>
    /// Middleware global que captura exceções não tratadas e retorna
    /// respostas padronizadas no formato ProblemDetails (RFC 7807).
    /// Registrar em Program.cs com: app.UseMiddleware&lt;ExceptionHandlerMiddleware&gt;();
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
 
        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exceção não tratada: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
 
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";
 
            var (statusCode, title) = exception switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, "Requisição inválida"),
                KeyNotFoundException => (HttpStatusCode.NotFound, "Recurso não encontrado"),
                UnauthorizedAccessException => (HttpStatusCode.Forbidden, "Acesso negado"),
                InvalidOperationException => (HttpStatusCode.Conflict, "Operação inválida"),
                _ => (HttpStatusCode.InternalServerError, "Erro interno do servidor")
            };
 
            context.Response.StatusCode = (int)statusCode;
 
            var problemDetails = new
            {
                type = $"https://tools.ietf.org/html/rfc9110#section-15.{(int)statusCode}",
                title,
                status = (int)statusCode,
                detail = exception.Message,
                traceId = context.TraceIdentifier
            };
 
            var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
 
            await context.Response.WriteAsync(json);
        }
    }
}