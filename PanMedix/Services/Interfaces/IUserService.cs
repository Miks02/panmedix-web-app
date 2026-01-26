using PanMedix.Enums;
using PanMedix.Models;

namespace PanMedix.Services.Interfaces;

public interface IUserService
{
    Task<User> AddUserAsync(User user, UserRole role, string? password);
}