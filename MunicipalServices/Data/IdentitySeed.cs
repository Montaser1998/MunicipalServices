using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalServices.Data
{
    public class IdentitySeed
    {
        public static async Task SeedRolesAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole("الادارة"));
            await roleManager.CreateAsync(new IdentityRole("قسم المالية"));
            await roleManager.CreateAsync(new IdentityRole("قسم الهندسة"));
            await roleManager.CreateAsync(new IdentityRole("قسم المياه"));
        }
        public static async Task SeedAdminAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new Users
            {
                UserName = "admin",
                Email = "Admin@default.com",
                FullName = "Admin",
                CreatedDate = DateTime.UtcNow,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456");
                    await userManager.AddToRoleAsync(defaultUser, "الادارة");
                    await userManager.AddToRoleAsync(defaultUser, "قسم المالية");
                    await userManager.AddToRoleAsync(defaultUser, "قسم الهندسة");
                    await userManager.AddToRoleAsync(defaultUser, "قسم المياه");
                }

            }
        }
        public static async Task SeedFinanceUserAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new Users
            {
                UserName = "finance",
                Email = "Finance@default.com",
                FullName = "Finance",
                CreatedDate = DateTime.UtcNow,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456");
                    await userManager.AddToRoleAsync(defaultUser, "قسم المالية");
                }

            }
        }
        public static async Task SeedEngineeringUserAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new Users
            {
                UserName = "engineering",
                Email = "Engineering@default.com",
                FullName = "Engineering",
                CreatedDate = DateTime.UtcNow,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456");
                    await userManager.AddToRoleAsync(defaultUser, "قسم الهندسة");
                }

            }
        }
        public static async Task SeedWaterUserAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new Users
            {
                UserName = "employeeWater",
                Email = "EmployeeWater@default.com",
                FullName = "EmployeeWater",
                CreatedDate = DateTime.UtcNow,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456");
                    await userManager.AddToRoleAsync(defaultUser, "قسم المياه");
                }

            }
        }
    }
}
