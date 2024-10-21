using MediatR;
using RentalCar.Security.Application.Commands.Response.Users;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Users
{
    public class UpdateUserRequest : IRequest<ApiResponse<InputUserResponse>>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
