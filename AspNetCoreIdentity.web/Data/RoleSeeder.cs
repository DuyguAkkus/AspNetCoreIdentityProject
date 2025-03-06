using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitiy.web.Data;

public class RoleSeeder
{
    public static async Task SeedRoles(RoleManager<AppRole> roleManager)
    {
        var roles = new List<AppRole>
        {
            new AppRole { Name = "Admin", AuthorityLevel = 2 },
            new AppRole { Name = "Teacher", AuthorityLevel = 3 },
            new AppRole { Name = "Student", AuthorityLevel = 4 }
        };

        foreach (var role in roles)
        {
            var existingRole = await roleManager.FindByNameAsync(role.Name);
            if (existingRole == null)
            {
                role.ConcurrencyStamp = Guid.NewGuid().ToString(); // ✅ ConcurrencyStamp set ediliyor
                await roleManager.CreateAsync(role);
            }
            else
            {
                bool updated = false;

                if (existingRole.AuthorityLevel != role.AuthorityLevel)
                {
                    existingRole.AuthorityLevel = role.AuthorityLevel;
                    updated = true;
                }

                if (string.IsNullOrEmpty(existingRole.ConcurrencyStamp))
                {
                    existingRole.ConcurrencyStamp = Guid.NewGuid().ToString(); // ✅ Eğer NULL ise set et
                    updated = true;
                }

                if (updated)
                {
                    await roleManager.UpdateAsync(existingRole);
                }
            }
        }
    }
}