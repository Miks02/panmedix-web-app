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
    
    [HttpGet]
    public IActionResult Login()
    {
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

        return View();

    }

    [HttpGet]
    public IActionResult Register()
    {
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
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
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
    
}