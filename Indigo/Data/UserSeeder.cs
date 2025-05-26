using Indigo.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Indigo.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            // userManager служи за създаване, редактиране и изтриване на потребителски профили
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //await CreateUserWithRole(userManager, "admin@indigo.com", "Admin123!", Roles.Admin);

            var adminEmail = configuration["AdminUser:Email"];
            var adminPassword = configuration["AdminUser:Password"];
            await CreateUserWithRole(userManager, adminEmail, adminPassword, Roles.Admin);

            await CreateUserWithRole(userManager, "publisher@indigo.com", "Publisher123!", Roles.Publisher);
            await CreateUserWithRole(userManager, "author@indigo.com", "Author123!", Roles.Author);
            await CreateUserWithRole(userManager, "reviewer@indigo.com", "Reviewer123!", Roles.Reviewer);
        }

        public static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, 
            string password, 
            string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email,
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
