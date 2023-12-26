using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Exceptions;
using DigitalIndoor.Services;
using System.Text.Json;

namespace DigitalIndoor.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogService logService;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            this.logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ToException ex)
            {
                await HandleCustomError(context, ex.Message, writeLog: false);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var message = $"{exception.Message} \n{exception.StackTrace}";
            if (exception.InnerException is not null)
                message = $"{message}\n {exception.InnerException.Message}\n {exception.InnerException.StackTrace}";

            return HandleCustomError(context, message);
        }

        private Task HandleCustomError(HttpContext context, string message, bool writeLog = true)
        {
            var response = new MessageViewDto(message);
            var payload = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            if (writeLog)
                logService.Crash(message);
            return context.Response.WriteAsync(payload);
        }
    }
}
