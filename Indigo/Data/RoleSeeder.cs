using Indigo.Constants;
using Microsoft.AspNetCore.Identity;

namespace Indigo.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            // roleManager служи за създаване, редактиране и изтриване на роли
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Publisher))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Publisher));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Author))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Author));
            }
        }
    }
}
