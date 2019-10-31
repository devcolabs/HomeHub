using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeHubApiServer.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeHubApiServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (InitializeConsole())
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        private static bool InitializeConsole()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                Console.WriteLine("Application is already running");
                Console.ReadKey();
                return false;
            }

            ManageConsoleWindow.HideWindow();

            return true;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
