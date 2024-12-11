using MediatR;
using RentalCar.Security.Application.Commands.Response.Account;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Account;

public class UpdateAccountRequest : IRequest<ApiResponse<InputAccountResponse>>
{
    public string Id { get; set; } = string.Empty;
    public string IdUser { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}