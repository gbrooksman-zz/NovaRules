using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace NovaRules.BRE
{
    public static class LoggingExtension
    {
        public static Microsoft.Extensions.Hosting.IHostBuilder UseGloabalLogger(this Microsoft.Extensions.Hosting.IHostBuilder app)
        {
            return app.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .WriteTo.File(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\LOG\\" + string.Format("Logging-{0}.txt", DateTime.Now.ToString("yyyyMMdd")))
                    .WriteTo.Debug()
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"));
        }

    }
}
