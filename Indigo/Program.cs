using Indigo.Constants;
using Indigo.Data;
using Indigo.Models;
using Indigo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Indigo.Repositories;
using Indigo.Repositories.Interfaces;

namespace Indigo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IJournalRepository, JournalRepository>();
            builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
            builder.Services.AddScoped<ILiteratureRepository, LiteratureRepository>();
            builder.Services.AddScoped<IKeyWordRepository, KeyWordRepository>();
            builder.Services.AddScoped<IPublicationReviewRepository, PublicationReviewRepository>();
            builder.Services.AddScoped<ITomeRepository, TomeRepository>();
            builder.Services.AddScoped<IPartRepository, PartRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                RoleSeeder.SeedRolesAsync(services).Wait();
                UserSeeder.SeedUsersAsync(services, configuration).Wait();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
