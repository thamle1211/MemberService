using System.Diagnostics;

namespace MemberService.Api.Middleware;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var correlationId = Guid.NewGuid().ToString();
        context.Response.Headers.Append("X-Correlation-ID", correlationId);

        var sw = Stopwatch.StartNew();
        _logger.LogInformation("Handling request {Method} {Path} - CorrelationId: {CorrelationId}",
            context.Request.Method, context.Request.Path, correlationId);

        await _next(context);

        sw.Stop();
        _logger.LogInformation("Finished request {Method} {Path} in {ElapsedMilliseconds}ms - CorrelationId: {CorrelationId}",
            context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds, correlationId);
    }
}