using Indigo.Constants;
using Microsoft.AspNetCore.Identity;

namespace Indigo.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
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

            if (!await roleManager.RoleExistsAsync(Roles.Reviewer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Reviewer));
            }
        }
    }
}
