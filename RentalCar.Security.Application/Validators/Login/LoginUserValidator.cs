using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Login;

namespace RentalCar.Security.Application.Validators.Login;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator() 
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Informa o e-mail")
            .EmailAddress().WithMessage("Informa um email com o formato correcto");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Informa a senha");
    }
}