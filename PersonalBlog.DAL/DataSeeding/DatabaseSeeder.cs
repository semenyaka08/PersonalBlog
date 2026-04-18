using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PersonalBlog.DAL.Constants;
using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL.DataSeeding;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(RoleManager<ApplicationRole> roleManager, ILogger<DatabaseSeeder> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        await SeedRolesAsync();
    }
    
    private async Task SeedRolesAsync()
    {
        var roles = Enum.GetNames<AppRoles>();

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new ApplicationRole(roleName));
                
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Role '{roleName}' successfully created.");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError($"An Error while creating a role: '{roleName}': {errors}");
                }
            }
        }
    }
}