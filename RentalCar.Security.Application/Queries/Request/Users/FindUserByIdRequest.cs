using MediatR;
using RentalCar.Security.Application.Queries.Response.Users;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Queries.Request.Users
{
    public class FindUserByIdRequest(string id) : IRequest<ApiResponse<FindUserResponse>>
    {
        public string Id { get; set; } = id;
    }
}
