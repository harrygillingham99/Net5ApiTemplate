using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Events;
using System.Linq;

namespace NetCore5ApiTemplate
{
    public class Program
    {
        private static readonly LogEventLevel[] errorLogs = new[] { LogEventLevel.Error, LogEventLevel.Fatal };
        private static readonly LogEventLevel[] infoLogs = new[] { LogEventLevel.Information, LogEventLevel.Warning, LogEventLevel.Debug };

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .Enrich.FromLogContext()
                 .WriteTo.Logger(lc => lc
                     .Filter.ByIncludingOnly(ev => errorLogs.Contains(ev.Level))
                     .WriteTo.File(
                         new JsonFormatter(renderMessage: true),
                         Path.Combine(AppContext.BaseDirectory, "logs//Serilog.json"),
                         shared: true,
                         fileSizeLimitBytes: 20_971_520, 
                         rollOnFileSizeLimit: true,
                         retainedFileCountLimit: 10))
                 .WriteTo.Logger(lc => lc
                     .Filter.ByIncludingOnly(ev => infoLogs.Contains(ev.Level))
                     .WriteTo.File(
                         new JsonFormatter(renderMessage: true),
                         Path.Combine(AppContext.BaseDirectory, "logs//Info.json"),
                         shared: true,
                         fileSizeLimitBytes: 20_971_520,
                         rollOnFileSizeLimit: true,
                         retainedFileCountLimit: 10))
                 .CreateLogger();

            try
            {
                Log.Information("Starting Application");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
