using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace MyKitchen.Logging;

public class CustomDebugLogger : ILogger
{
    private readonly string _categoryName;
    private readonly Func<object,Exception,string> _formatter;
    private IConfiguration Configuration { get; } 

    public CustomDebugLogger(string categoryName, IConfiguration config)
    {
        this._categoryName = categoryName;
        _formatter = CustomLogMessageFormatter;
        Configuration = config;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        // You can implement scope management if needed
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {

        //check for log level _categoryName
        var configuredLogLevel = Configuration.GetValue<string>($"Logging:Debug:LogLevel:{_categoryName}");

        //if not found, use default
        if (string.IsNullOrEmpty(configuredLogLevel))
        {
            configuredLogLevel = Configuration.GetValue<string>($"Logging:Debug:LogLevel:Default");
        }

        // Parse the configured log level
        if (Enum.TryParse(configuredLogLevel, out LogLevel configuredLogLevelEnum))
        {
            // Check if the log level is enabled based on the configured level
            return logLevel >= configuredLogLevelEnum;
        }

        return true; // Log all if configuration is not properly set
    }

    private  string CustomLogMessageFormatter<TState>(TState state, Exception exception)
    {
        string message;

        if(exception != null)
        {
            message = exception.Message + exception.StackTrace;
        }
        else
        {
            message = state.ToString();
        }

        // Customize the log message format or perform additional logic
        if(!_categoryName.Contains("MyKitchen")){

            message = $"[{_categoryName}] {message}";

        }

        return message;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        //discard this parameter
        // ReSharper disable once RedundantAssignment
        formatter = null;

        if (!IsEnabled(logLevel))
        {
            return;
        }
        var formattedMessage = _formatter(state, exception);

        // Write the custom formatted message to the debug output
        Debug.WriteLine(formattedMessage);
    }
}