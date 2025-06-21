using Microsoft.AspNetCore.Identity;
using OurEstheticSolution.Data;
using OurEstheticSolution.Models;

namespace OurEstheticSolution.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();
            try
            { // Ensure the database is ready
                logger.LogInformation("Ensuring the database is created");

                await context.Database.EnsureCreatedAsync();

                //Add roles
                logger.LogInformation("Seeding roles.");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "User");

                // Add admin user
                
                logger.LogInformation("Seeding admin user.");
                var adminEmail = "srijana@gmail.com";
                var adminUserName = "srijana";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new User
                    {
                        FullName = "Code Hub",
                        UserName = adminUserName,
                        Email = adminEmail,
                        DateOfBirth = new DateTime(1990, 1, 1),
                        NormalizedEmail = adminEmail.ToUpper(),
                        NormalizedUserName = adminUserName.ToUpper(),
                        EmailConfirmed = true,
                        createdAt = DateTime.UtcNow,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Address = "123 Code Street, Tech City, 12345",
                        Gender = "Female"
                    };

                    var result = await userManager.CreateAsync(adminUser,"Admin@123");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assigning Admin role to the admin user" +
                            ".");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the database.");
                throw;
            }

        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {

                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

            }
        }
    }
}
