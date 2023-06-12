using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace MyKitchen.Middleware;

public class EndpointLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public EndpointLoggerMiddleware(RequestDelegate next, ILogger<EndpointLoggerMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpointFeature = context.Features[typeof(IEndpointFeature)] as IEndpointFeature;
        Endpoint endpoint = endpointFeature?.Endpoint as Endpoint;

        if (endpoint != null)
        {
            var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;


            _logger.LogInformation("Name: " + endpoint.DisplayName);
            _logger.LogInformation($"Route Pattern: {routePattern}");
            _logger.LogInformation("Metadata Types: " + string.Join(", ", endpoint.Metadata));
        }

        await _next(context);
    }
}