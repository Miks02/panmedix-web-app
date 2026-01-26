using Microsoft.AspNetCore.Identity;
using PanMedix.Models;

namespace PanMedix.Helpers;

public static class Seeder
{
    public static async Task SeedAdmin(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
   
        string roleName =  "Admin";
       
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new IdentityRole(roleName));

        var adminEmail = configuration["AdminData:Email"];

        if (string.IsNullOrWhiteSpace(adminEmail))
            throw new ArgumentNullException(nameof(adminEmail), "Prosledite e-mail adresu za korisnika");
        
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new User()
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = configuration["AdminData:FirstName"]!,
                LastName = configuration["AdminData:LastName"]!,
                EmailConfirmed = true,
                
            };
            await userManager.CreateAsync(adminUser, configuration["AdminData:Password"]!);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        
    }
}