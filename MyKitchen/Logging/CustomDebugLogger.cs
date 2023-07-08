public class CustomDebugLogger : ILogger
{
    private readonly string categoryName;
    private readonly Func<Object,Exception,string> _formatter;

    public CustomDebugLogger(string categoryName)
    {
        this.categoryName = categoryName;
        _formatter = CustomLogMessageFormatter;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        // You can implement scope management if needed
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        // Customize log level filtering if needed
        return true;
    }

    private  string CustomLogMessageFormatter<TState>(TState state, Exception exception)
    {
        string catName = string.Empty;
        string message = string.Empty;

        if(exception != null)
        {
            message = exception.Message + exception.StackTrace;
        }
        else
        {
            message = state.ToString();
        }

        // Customize the log message format or perform additional logic
        if(!categoryName.Contains("MyKitchen")){

            message = $"[{categoryName}] {message}";

        }

        return message;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        //discard parm
        formatter = null;

        var formattedMessage = _formatter(state, exception);


        // Write the custom formatted message to the debug output
        System.Diagnostics.Debug.WriteLine(formattedMessage);
    }
}
