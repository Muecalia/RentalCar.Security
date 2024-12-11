using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Roles;

namespace RentalCar.Security.Application.Validators.Roles;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleValidator() 
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Informe o nome");
    }
}