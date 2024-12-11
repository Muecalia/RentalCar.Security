using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Account;

namespace RentalCar.Security.Application.Validators.Account;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("O código do utilizador não pode estar vazio")
            .Length(36).WithMessage("O código do utilizador não está no formato permitido");

        RuleFor(c => c.NewPassword)
            .NotEmpty().WithMessage("A nova senha não pode estar vazia")
            .Length(4, 20).WithMessage("O tamanho de caracteres do nome deve estar entre 4 e 20");

        RuleFor(c => c.OldPassword)
            .NotEmpty().WithMessage("A antiga senha não pode estar vazia");
    }
}