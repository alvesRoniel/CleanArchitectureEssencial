using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMVC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //adicionando o Serilog
                .ConfigureAppConfiguration((config) =>
                {
                    var seting = config.Build();

                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.MSSqlServer(
                        seting.GetConnectionString("DefaultConnection"),
                        sinkOptions: new MSSqlServerSinkOptions()
                        {
                            TableName = "Serilogs",
                            AutoCreateSqlTable = true
                        })
                    .CreateLogger();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
