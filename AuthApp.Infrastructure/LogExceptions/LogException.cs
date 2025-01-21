using Serilog;



namespace AuthApp.Infrastructure.LogExceptions
{
    public static class LogException
    {
        public static void LogEx(Exception ex)
        {
            Log.Warning(ex.Message);
            Log.Debug(ex.Message);
        }
    }
}
