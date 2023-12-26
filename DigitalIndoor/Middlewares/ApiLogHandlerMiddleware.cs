using DigitalIndoor.Services;

namespace DigitalIndoor.Middlewares
{
    public class ApiLogHandlerMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogService logService;

        public ApiLogHandlerMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            this.logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            var log = $"{context.Request.Protocol} # {context.Connection.RemoteIpAddress} # {context.Request.Method} # {context.Request.Path} # {context.Request.QueryString.Value}";

            logService.Log(log);
            await _next(context);
        }
    }
}
