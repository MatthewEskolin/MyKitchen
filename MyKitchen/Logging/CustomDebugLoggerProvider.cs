using System.Configuration;
using System.Drawing;
using Microsoft.Extensions.Logging;
using MyKitchen.Logging;

public class CustomDebugLoggerProvider : ILoggerProvider
{
    private IConfiguration Configuration { get; }

    public CustomDebugLoggerProvider(IConfiguration config)
    {
        Configuration = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new CustomDebugLogger(categoryName,Configuration);
    }
    
    public void Dispose()
    {
        // Cleanup resources if needed
    }
}
