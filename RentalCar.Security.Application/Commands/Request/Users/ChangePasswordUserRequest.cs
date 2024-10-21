using MediatR;
using RentalCar.Security.Application.Commands.Response.Users;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Users
{
    public class ChangePasswordUserRequest : IRequest<ApiResponse<InputUserResponse>>
    {
        public string Id { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
