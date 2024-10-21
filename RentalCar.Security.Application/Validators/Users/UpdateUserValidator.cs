using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Users;

namespace RentalCar.Security.Application.Validators.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Informe o código do utilizador")
                .Length(36).WithMessage("Formato do código inválido");

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Informe o nome")
                .Length(2, 100).WithMessage("O tamanho dos caracteres do nome tem de estar entre 2 - 100");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Informe o e-mail")
                .EmailAddress().WithMessage("O formato do e-mail não é válido");

            RuleFor(u => u.Phone)
                .NotEmpty()
                .WithMessage("Informe o telefone");
        }
    }
}
