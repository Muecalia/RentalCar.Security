using MediatR;
using RentalCar.Security.Application.Commands.Response.Account;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Account;

public class DeleteAccountRequest(string id) : IRequest<ApiResponse<InputAccountResponse>>
{
    public string Id { get; set; } = id;
}