using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalBlog.DAL.Constants;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL;

public class DatabaseSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var logger = serviceProvider.GetRequiredService<ILogger<DatabaseSeeder>>();
        
        var roles = Enum.GetNames<AppRoles>();

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                
                if (result.Succeeded)
                {
                    logger.LogInformation($"Role '{roleName}' successfully created.");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    logger.LogError($"An Error while creating a role: '{roleName}': {errors}");
                }
            }
        }
    }
}