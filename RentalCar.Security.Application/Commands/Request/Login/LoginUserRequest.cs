using MediatR;
using RentalCar.Security.Application.Commands.Response.Login;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Login
{
    public class LoginUserRequest : IRequest<ApiResponse<LoginUserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
