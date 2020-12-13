using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MunicipalServices
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<Data.ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<Data.Users>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await Data.IdentitySeed.SeedRolesAsync(userManager, roleManager);
                    await Data.IdentitySeed.SeedAdminAsync(userManager, roleManager);
                    await Data.IdentitySeed.SeedEngineeringUserAsync(userManager, roleManager);
                    await Data.IdentitySeed.SeedFinanceUserAsync(userManager, roleManager);
                    await Data.IdentitySeed.SeedWaterUserAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
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
