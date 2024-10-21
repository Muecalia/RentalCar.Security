using MediatR;
using RentalCar.Security.Application.Queries.Response.Roles;
using RentalCar.Security.Application.Wrappers;

namespace RentalCar.Security.Application.Queries.Request.Roles
{
    public class FindAllRolesRequest : IRequest<PagedResponse<FindRoleResponse>>
    {
    }
}
