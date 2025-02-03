using Microsoft.AspNetCore.Identity;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Data.SeedDb
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Client", "Seer" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);

                    await roleManager.CreateAsync(role);
                }
            }
        }
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@domain.com");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com",
                    AccountPhoto = "AdminDefault.png",
                    FirstName = "Админ",
                    LastName = "Админов"
                };

                var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var regularUser = await userManager.FindByEmailAsync("client@domain.com");

            if (regularUser == null)
            {
                regularUser = new ApplicationUser
                {
                    UserName = "client@domain.com",
                    Email = "client@domain.com",
                    AccountPhoto = "RegularDefault.png",
                    FirstName = "Петър",
                    LastName = "Дамянов"
                };

                var result = await userManager.CreateAsync(regularUser, "ClientPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "Client");
                }
            }

            var seer = await userManager.FindByEmailAsync("seer@domain.com");

            if (seer == null)
            {
                seer = new ApplicationUser
                {
                    UserName = "seer@domain.com",
                    Email = "seer@domain.com",
                    AccountPhoto = "SeerDefault.png",
                    FirstName = "Иван",
                    LastName = "Мистични"
                };

                var result = await userManager.CreateAsync(seer, "SeerPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(seer, "Seer");
                }

                var seerEntry = new Seer()
                {
                    ApplicationUserId = seer.Id,
                    ApplicationUser = seer
                };

                seer.Seer = seerEntry;

                await context.Seers.AddAsync(seerEntry);

                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedDataAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            await SeedRolesAsync(roleManager);

            await SeedUsersAsync(userManager, context);
        }
    }
}
