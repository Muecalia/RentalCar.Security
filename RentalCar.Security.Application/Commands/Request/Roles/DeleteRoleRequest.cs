using MediatR;
using RentalCar.Security.Application.Commands.Response.Roles;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Roles;

public class DeleteRoleRequest(string id) : IRequest<ApiResponse<InputRoleResponse>>
{
    public string Id { get; set; } = id;
}