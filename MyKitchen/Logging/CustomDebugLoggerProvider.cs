using Microsoft.Extensions.Logging;
using MyKitchen.Logging;

public class CustomDebugLoggerProvider : ILoggerProvider
{
    public CustomDebugLoggerProvider()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new CustomDebugLogger(categoryName);
        
    }
    
    public void Dispose()
    {
        // Cleanup resources if needed
    }
}
