using MediatR;
using RentalCar.Security.Application.Commands.Response.Account;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Commands.Request.Account;

public class ChangePasswordRequest : IRequest<ApiResponse<InputAccountResponse>>
{
    public string Id { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}