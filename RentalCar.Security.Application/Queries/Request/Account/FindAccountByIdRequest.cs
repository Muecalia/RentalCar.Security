using MediatR;
using RentalCar.Security.Application.Queries.Response.Account;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Queries.Request.Account;

public class FindAccountByIdRequest(string id) : IRequest<ApiResponse<FindAccountResponse>>
{
    public string Id { get; set; } = id;
}