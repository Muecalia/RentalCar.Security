using MediatR;
using RentalCar.Security.Application.Commands.Response.Login;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Login;

public class LoginUserRequest : IRequest<ApiResponse<LoginUserResponse>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}