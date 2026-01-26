using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using PanMedix.Enums;
using PanMedix.Exceptions.User;
using PanMedix.Models;
using PanMedix.Services.Interfaces;
using PanMedix.ViewModels;

namespace PanMedix.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;

    public AuthService(
        IUserService userService,
        SignInManager<User> signInManager
        )
    {
        _userService = userService;
        _signInManager = signInManager;
    }
    
    public async Task RegisterAsync(RegisterViewModel request)
    {
        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email
        };

        var newUser = await _userService.AddUserAsync(user, UserRole.User, request.Password);

        await _signInManager.SignInAsync(newUser, isPersistent: false);

    }

    public async Task LoginAsync(LoginViewModel request)
    {
        throw new NotImplementedException();
    }
}