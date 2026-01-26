using FluentValidation;
using PanMedix.ViewModels;

namespace PanMedix.Validators;

public class RegisterValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Ime je obavezno")
            .MinimumLength(2)
            .WithMessage("Ime morа sadržati barem 2 karaktera")
            .MaximumLength(50)
            .WithMessage("Ime ne sme imati više od 50 karaktera");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Prezime je obavezno")
            .MinimumLength(3)
            .WithMessage("Prezime morа sadržati barem 3 karaktera")
            .MaximumLength(50)
            .WithMessage("Prezime ne sme imati više od 50 karaktera");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail adresa je obavezna")
            .EmailAddress()
            .WithMessage("E-mail adresa je nevalidna");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Lozinka je obavezna")
            .MinimumLength(6)
            .WithMessage("Lozinka mora sadržati barem 6 karaktera");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Potvrdite lozinku")
            .Equal(x => x.Password)
            .WithMessage("Lozinke se ne poklapaju");

    }
}