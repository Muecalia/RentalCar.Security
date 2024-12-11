using MediatR;
using RentalCar.Security.Application.Queries.Response.Account;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Queries.Request.Account;

public class FindAllAccountsRequest : IRequest<PagedResponse<FindAllAccountsResponse>>
{
    
}