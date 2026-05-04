using System.Diagnostics;

namespace SPR411_SteamClone.API.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("[START] Запит: {Method} {Path} розпочато о {StartTime}", 
                context.Request.Method, context.Request.Path, startTime.ToString("HH:mm:ss.fff"));

            await _next(context);

            stopwatch.Stop();
            var endTime = DateTime.UtcNow;

            _logger.LogInformation("⬅️ [END] Відповідь: {StatusCode} для {Method} {Path} відправлено о {EndTime}. Час виконання: {ElapsedMilliseconds} мс", 
                context.Response.StatusCode, context.Request.Method, context.Request.Path, endTime.ToString("HH:mm:ss.fff"), stopwatch.ElapsedMilliseconds);
        }
    }
}