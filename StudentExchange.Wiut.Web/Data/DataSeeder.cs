using Microsoft.AspNetCore.Identity;
using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<Student> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminUser = new Student
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true,
                FamilyName = "Admin",
                DateOfBirth = DateTime.Now,
                ForeName = "Admin",
                Title = "Title"
            };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                await userManager.CreateAsync(adminUser, "WestExchangeAdmin7");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

}
