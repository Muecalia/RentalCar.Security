using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Account;

namespace RentalCar.Security.Application.Validators.Account;

public class DeleteAccountValidator : AbstractValidator<DeleteAccountRequest>
{
    public DeleteAccountValidator() 
    {
        RuleFor(u => u.Id)
            .NotEmpty().WithMessage("Informe o código do utilizador")
            .Length(36).WithMessage("Formato do código inválido");
    }
}