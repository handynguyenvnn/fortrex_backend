using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace StockTickR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            var config = new ConfigurationBuilder()
                 .AddCommandLine(args)
                 .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .ConfigureLogging(factory =>
                {
                    factory.AddConsole();
                })
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIIS()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:4235")
                //.UseUrls("http://wss.forbitoption.com")
                .Build();

            //edit to
            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseIIS()
            //    .UseIISIntegration()
            //       .ConfigureLogging(logging =>
            //       {
            //           logging.ClearProviders();
            //           logging.AddConsole();
            //           logging.SetMinimumLevel(LogLevel.Warning);
            //           logging.AddEventLog();

            //           logging.AddEventSourceLogger();
            //           logging.AddFilter("System", LogLevel.Debug)
            //            .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace);
            //       })
            //    .UseConfiguration(config)
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .Build();
            host.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
