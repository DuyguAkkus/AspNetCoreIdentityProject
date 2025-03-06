using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitiy.web.Data;

public class RoleSeeder
{
    public static async Task SeedRoles(RoleManager<AppRole> roleManager)
    {
        var roles = new List<AppRole>
        {
            new AppRole { Name = "Admin", NormalizedName = "ADMIN", AuthorityLevel = 2 },
            new AppRole { Name = "Teacher", NormalizedName = "TEACHER", AuthorityLevel = 3 },
            new AppRole { Name = "Student", NormalizedName = "STUDENT", AuthorityLevel = 4 }
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}