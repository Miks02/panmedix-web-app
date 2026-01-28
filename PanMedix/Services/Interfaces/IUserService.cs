using PanMedix.Enums;
using PanMedix.Models;

namespace PanMedix.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<User> AddUserAsync(User user, UserRole role, string? password);
    Task<bool> IsUserRegisteredAsGuardian(User user);
}