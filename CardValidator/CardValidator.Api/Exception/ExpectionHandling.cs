using System.Net;
using System.Text.Json;

namespace CardValidator.Api.Middleware {
    public class ExceptionHandling {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;

        public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var payload = JsonSerializer.Serialize(new {
                    error = "An unexpected error occurred. Please try again later."
                });

                await context.Response.WriteAsync(payload);
            }
        }
    }

    public static class ExceptionHandlingExtensions {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ExceptionHandling>();
        }
    }
}
