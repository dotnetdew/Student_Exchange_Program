using Microsoft.AspNetCore.Identity;
using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<Student> userManager, RoleManager<IdentityRole> roleManager)
    {
        await CreateRoleIfNotExists(roleManager, "Student");
        await CreateRoleIfNotExists(roleManager, "Admin");
        await CreateRoleIfNotExists(roleManager, "Coordinator");

        await CreateUserIfNotExists(userManager, "admin@example.com", "WestExchangeAdmin7", "Admin");
        await CreateUserIfNotExists(userManager, "rkhamraeva@wiut.uz", "khamraevaR7", "Coordinator");
    }

    private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                throw new Exception($"Failed to create role {roleName}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
    }

    private static async Task CreateUserIfNotExists(UserManager<Student> userManager, string userName, string password, string roleName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new Student
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true,
                FamilyName = userName.Contains("admin") ? "Admin" : "Khamraeva",
                DateOfBirth = DateTime.Now,
                ForeName = userName.Contains("admin") ? "Admin" : "Rukhsana",
                Title = userName.Contains("admin") ? "Title" : "Ms."
            };

            var createResult = await userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, roleName);
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to add user {userName} to role {roleName}: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                throw new Exception($"Failed to create user {userName}: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}
