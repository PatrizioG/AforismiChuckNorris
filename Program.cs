using AforismiChuckNorris.Data;
using AforismiChuckNorris.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AforismiChuckNorris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var aphorismService = scope.ServiceProvider.GetRequiredService<IAphorismsService>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                var path = $"{env.ContentRootPath}\\Data\\seedData.txt";

                // Create the database if it doesn't exist
                context.Database.EnsureCreated();

                SeedData.SeedAphorisms(logger, context, aphorismService, path);
                context.Dispose();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
