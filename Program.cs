using ChuckNorrisAphorisms.Data;
using ChuckNorrisAphorisms.Data.Entities;
using ChuckNorrisAphorisms.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{

            //    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    var aphorismService = scope.ServiceProvider.GetRequiredService<IAphorismsService>();
            //    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            //    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            //    var path = $"{env.ContentRootPath}\\Data\\seedData.txt";

            //    await context.Database.EnsureCreatedAsync();

            //    await SeedData.SeedAphorismsAsync(logger, context, aphorismService, path);

            //    await CreateAdminUser(scope.ServiceProvider);

            //    context.Dispose();
            //}

            await host.RunAsync();
        }

        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await RoleManager.RoleExistsAsync("Administrator"))
                await RoleManager.CreateAsync(new IdentityRole("Administrator"));

            ApplicationUser user = await UserManager.FindByEmailAsync("patrizio.gasperi@gmail.com");

            if (user != null)
                await UserManager.AddToRoleAsync(user, "Administrator");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
