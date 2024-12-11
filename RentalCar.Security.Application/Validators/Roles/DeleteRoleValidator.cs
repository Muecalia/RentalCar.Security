using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Roles;

namespace RentalCar.Security.Application.Validators.Roles;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleRequest>
{
    public DeleteRoleValidator() 
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage("Informa o código do utilizador");
    }
}