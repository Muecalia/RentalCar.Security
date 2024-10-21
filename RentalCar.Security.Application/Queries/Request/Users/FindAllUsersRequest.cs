using MediatR;
using RentalCar.Security.Application.Queries.Response.Users;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Queries.Request.Users
{
    public class FindAllUsersRequest : IRequest<PagedResponse<FindAllUsersResponse>>
    {
    }
}
