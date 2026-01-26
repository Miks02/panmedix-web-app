using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PanMedix.Exceptions.User;
using PanMedix.Services.Interfaces;
using PanMedix.ViewModels;

namespace PanMedix.Controllers;

public class AuthController : Controller
{
    private readonly IValidator<LoginViewModel> _loginValidator;
    private readonly IValidator<RegisterViewModel> _registerValidator;
    private readonly IAuthService _authService;
    
    public AuthController(
        IValidator<LoginViewModel> loginValidator,
        IValidator<RegisterViewModel> registerValidator,
        IAuthService authService
        )
    {
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _authService = authService;
    }

    private string GetCurrentUser()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        if (!string.IsNullOrWhiteSpace(GetCurrentUser()))
            return RedirectToAction("Index", "Home");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel request)
    {
        var result = await _loginValidator.ValidateAsync(request);

        if (!result.IsValid)
        {
            ModelState.Clear();
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            return View(request);
        }
        
        try
        {
            await _authService.LoginAsync(request);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(request.Password), ex.Message);
            return View(request);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (!string.IsNullOrWhiteSpace(GetCurrentUser()))
            return RedirectToAction("Index", "Home");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel request)
    {
        var result = await _registerValidator.ValidateAsync(request);

        if (!result.IsValid)
        {
            ModelState.Clear();
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            
            return View(request);
        }

        try
        {
            await _authService.RegisterAsync(request);
        }
        catch (UserAlreadyExistsException ex)
        {
            ModelState.AddModelError(nameof(request.Email), ex.Message);
            return View(request);
        }
        catch (InvalidOperationException)
        {
            TempData["ErrorMessage"] = "Došlo je do greške prilikom registracije, pokušajte ponovo kasnije";
            return View(request);
        }

        return RedirectToAction("Index", "Home");

    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();

        return View("Login");
    }
    
}