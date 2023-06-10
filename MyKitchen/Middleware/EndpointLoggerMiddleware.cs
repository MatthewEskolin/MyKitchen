using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;

namespace MyKitchen.Middleware;

public class EndpointLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpointFeature = context.Features[typeof(IEndpointFeature)] as IEndpointFeature;
        Endpoint endpoint = endpointFeature?.Endpoint as Endpoint;

        if (endpoint != null)
        {
            var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;

            Console.WriteLine("Name: " + endpoint.DisplayName);
            Console.WriteLine($"Route Pattern: {routePattern}");
            Console.WriteLine("Metadata Types: " + string.Join(", ", endpoint.Metadata));
        }

        await _next(context);
    }
}