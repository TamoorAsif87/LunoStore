using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
       

        var requestName = typeof(TRequest).Name;
        var requestContent = request;

        logger.LogInformation("➡️ Handling {RequestName} with content: {RequestContent}",
            requestName, requestContent);

        var stopwatch = Stopwatch.StartNew();
        double seconds;
        try
        {
            var response = await next();
            stopwatch.Stop();

            seconds = stopwatch.ElapsedMilliseconds / 1000.0;

            logger.LogInformation("✅ Handled {RequestName} in {seconds:F}s",
                requestName, seconds);

            return response;
        }
       
        catch (Exception ex)
        {
            stopwatch.Stop();
            seconds = stopwatch.ElapsedMilliseconds / 1000.0;

            logger.LogError(ex, "❌ {RequestName} failed after {seconds}ms",
                requestName, seconds);

            throw; // Let the exception propagate to the global handler
        }
    }
}
