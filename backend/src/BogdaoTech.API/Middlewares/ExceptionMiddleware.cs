using BogdaoTech.API.Response;
using System.Net;

namespace BogdaoTech.API.Middlewares
{
    public class ExceptionMiddleware
    {
        //делегат, который возвращает таск и принимает нттр контекст
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
                //вызов всего приложения
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var resopnseError = new ResponseError("server.internal", ex.Message, null);
                var envelope = Envelope.Error([resopnseError]);
                
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                
                await context.Response.WriteAsJsonAsync(envelope);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
