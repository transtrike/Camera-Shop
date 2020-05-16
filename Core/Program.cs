using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Camera_Shop
{
     public class Program
     {
          public static void Main(string[] args)
          {
#if _WIN32
               CreateHostBuilder(args).Build().Run();
#else
               var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();
            
               host.Run();
               
               /*
                * View - UI Elements
                * Model - Business logic, validation layer
                * Controller - User input parsing and interaction
                */
#endif
          }

          public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
     }
}