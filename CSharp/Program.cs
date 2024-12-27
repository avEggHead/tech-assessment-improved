using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace OrdersApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()  // Minimum log level to capture
                .WriteTo.Console()           // Write logs to the console
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Write logs to a file
                .CreateLogger();

            try
            {
                Log.Information("Starting up the Orders API...");
                CreateHostBuilder(args).Build().Run();
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

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>()
                        .UseSerilog();
				});
	}
}
