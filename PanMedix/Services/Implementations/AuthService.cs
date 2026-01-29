using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using PanMedix.Enums;
using PanMedix.Exceptions.Global;
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

        UserRole role = UserRole.User;

        if (request.IsGuardian)
            role = UserRole.Guardian;

        var newUser = await _userService.AddUserAsync(user, role, request.Password);

        await _signInManager.SignInAsync(newUser, isPersistent: false);

    }

    public async Task LoginAsync(LoginViewModel request)
    {
        var user = await _userService.GetUserByEmailAsync(request.Email);

        if (user is null)
            throw new InvalidOperationException("Korisničko ime ili lozinka nisu tačni");

        var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        
        if(!isPasswordValid.Succeeded)
            throw new InvalidOperationException("Korisničko ime ili lozinka nisu tačni");
        
        await _signInManager.SignInAsync(user, isPersistent: false);

    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}