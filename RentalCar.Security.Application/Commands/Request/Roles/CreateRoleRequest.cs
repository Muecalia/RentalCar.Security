using MediatR;
using RentalCar.Security.Application.Commands.Response.Roles;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Roles;

public class CreateRoleRequest : IRequest<ApiResponse<InputRoleResponse>>
{
    public string Name { get; set; } = string.Empty;
}