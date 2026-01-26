using FluentValidation;
using PanMedix.ViewModels;

namespace PanMedix.Validators;

public class LoginValidator : AbstractValidator<LoginViewModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail adresa je obavezna");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Lozinka je obavezna");
    }
}