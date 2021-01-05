using AforismiChuckNorris.Data;
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
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                var path = $"{env.ContentRootPath}\\Data\\seedData.txt";
                SeedData.SeedAphorisms(logger, context, path);
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
