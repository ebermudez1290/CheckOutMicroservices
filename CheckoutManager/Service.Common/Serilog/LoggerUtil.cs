using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace Service.Common.Serilog
{
    public static class LoggerUtil
    {
        public static void InitApp(Action function)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(new RenderedCompactJsonFormatter(), "./logs/Checkout.ndjson")
                .CreateLogger();
            try
            {
                Log.Information("Starting up");
                function();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
