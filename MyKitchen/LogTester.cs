namespace MyKitchen;

public class LogTester
{
    public static void TestLog(ILogger logger)
    {

        //TEST the Default Logging Configuration
        logger.LogTrace("Trace Test");
        logger.LogInformation("Information Test");
        logger.LogDebug("Debug Test");
        logger.LogWarning("Warning Test");
        logger.LogError("Error Test");
        logger.LogCritical("Critical Test");
    }
}
