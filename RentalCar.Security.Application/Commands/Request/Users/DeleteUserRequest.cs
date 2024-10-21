using MediatR;
using RentalCar.Security.Application.Commands.Response.Users;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Users
{
    public class DeleteUserRequest(string id) : IRequest<ApiResponse<InputUserResponse>>
    {
        public string Id { get; set; } = id;
    }
}
