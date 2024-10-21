using FluentValidation;
using RentalCar.Security.Application.Commands.Request.Roles;

namespace RentalCar.Security.Application.Validators.Roles
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator() 
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage("Informa o código do utilizador");

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Informe o nome");
        }
    }
}
