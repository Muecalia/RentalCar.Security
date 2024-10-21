using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Users;

namespace RentalCar.Security.Application.Validators.Users
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator() 
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Informe o código do utilizador")
                .Length(36).WithMessage("Formato do código inválido");
        }
    }
}
