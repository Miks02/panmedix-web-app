using Microsoft.AspNetCore.Identity;
using PanMedix.Enums;
using PanMedix.Models;

namespace PanMedix.Services.Interfaces;

public interface IRoleService
{
    Task<IdentityResult> AssignRoleAsync(User user, UserRole role);
}