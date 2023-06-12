namespace MyKitchen.Utilities
{
    public static class TraceUtilities
    {
        //For Logging before ILogger is available
        public static string TraceErrorIfNullOrEmpty(this string str,string name)
        {
            if (!string.IsNullOrEmpty(str)) return str;

            Trace.WriteLine($"ERROR: {name} is null or empty");
            return string.Empty;

        }
    }
}
