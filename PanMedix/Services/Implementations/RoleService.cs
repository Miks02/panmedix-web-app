using Microsoft.AspNetCore.Identity;
using PanMedix.Enums;
using PanMedix.Models;
using PanMedix.Services.Interfaces;

namespace PanMedix.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RoleService> _logger;

    public RoleService(
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager, 
        ILogger<RoleService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }
    
    public async Task<IdentityResult> AssignRoleAsync(User user, UserRole role)
    {
        var userRole = GetRole(role);
        var createRoleResult = await CreateRoleAsync(userRole);

        if (!createRoleResult.Succeeded)
        {
            _logger.LogError("Došlo je do greške prilikom kreiranja korisničke uloge, {role}", userRole);

            foreach (var error in createRoleResult.Errors)
                _logger.LogError("{code} | {description}", error.Code, error.Description);

            return IdentityResult.Failed(createRoleResult.Errors.ToArray());
        }

        var assignRoleResult = await _userManager.AddToRoleAsync(user, userRole);

        if (!assignRoleResult.Succeeded)
        {
            _logger.LogError("Došlo je do greške prilikom dodeljivanja korisničke uloge, {id}", user.Id);

            foreach (var error in createRoleResult.Errors)
                _logger.LogError("{code} | {description}", error.Code, error.Description);

            return IdentityResult.Failed(createRoleResult.Errors.ToArray());
        }

        return IdentityResult.Success;

    }

    private async Task<IdentityResult> CreateRoleAsync(string userRole)
    {

        if (await _roleManager.RoleExistsAsync(userRole))
            return IdentityResult.Success;

        var createResult = await _roleManager.CreateAsync(new IdentityRole()
        {
            Name = userRole,
            NormalizedName = userRole.ToUpper()
        });

        if (!createResult.Succeeded)
            return IdentityResult.Failed(createResult.Errors.ToArray());

        return IdentityResult.Success;

    }

    private static string GetRole(UserRole role)
    {
        return role switch
        {
            UserRole.Admin => "Admin",
            UserRole.Pharmacy => "Pharmacy",
            UserRole.Guardian => "Guardian",
            UserRole.User => "User",
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}