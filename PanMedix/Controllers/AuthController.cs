using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PanMedix.ViewModels;

namespace PanMedix.Controllers;

public class AuthController : Controller
{
    private readonly IValidator<LoginViewModel> _loginValidator;
    private readonly IValidator<RegisterViewModel> _registerValidator;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(
        IValidator<LoginViewModel> loginValidator,
        IValidator<RegisterViewModel> registerValidator,
        ILogger<AuthController> logger
        )
    {
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _logger = logger;
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
                _logger.LogError(error.ErrorMessage);
            }
            return View(request);
        }

        return View();

    }
    
}