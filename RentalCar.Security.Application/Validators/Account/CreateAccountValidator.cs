using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Account;

namespace RentalCar.Security.Application.Validators.Account;

public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountValidator() 
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("O nome não pode estar vazio")
            .Length(2, 100).WithMessage("O tamanho dos caracteres do nome tem de estar entre 2 - 100");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O e-mail não pode estar vazio")
            .EmailAddress().WithMessage("O formato do e-mail não é válido");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("A senha não pode estar vazia")
            .Length(4, 20).WithMessage("O tamanho dos caracteres na senha tem que estar de 4 - 20");

        RuleFor(u => u.Phone)
            .NotEmpty()
            .WithMessage("O telefone não pode estar vazio");

        RuleFor(u => u.Role)
            .NotEmpty()
            .WithMessage("Informa o perfil do utilizador");
    }
}