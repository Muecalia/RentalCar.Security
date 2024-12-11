using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Commands.Response.Account;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Account;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ApiResponse<InputAccountResponse>>
{
    private readonly IAccountRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public ChangePasswordHandler(IAccountRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<InputAccountResponse>> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        const string OBJECT = "account";
        const string OPERATION = "alterar senha";
        try
        {
            var account = await _repository.FindById(request.Id, cancellationToken);
            if (account == null)
            {
                _prometheusService.AddPasswordCounter(StatusCodes.Status404NotFound.ToString());
                _loggerService.LogWarning(MessageError.NotFound(OBJECT, request.Id));
                return ApiResponse<InputAccountResponse>.Error(MessageError.NotFound(OBJECT));
            }

            await _repository.ChangePassword(account, request.OldPassword, request.NewPassword, cancellationToken);
            var result = new InputAccountResponse(account.Id, account.Name, account.Email, account.CreatedAt.ToShortDateString());

            _prometheusService.AddAccountCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.OperacaoSucesso($"{OBJECT} {account.Name}", OPERATION));
            return ApiResponse<InputAccountResponse>.Success(result, MessageError.OperacaoSucesso(OBJECT, OPERATION));
        }
        catch (Exception ex)
        {
            _prometheusService.AddAccountCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(OBJECT, OPERATION), ex);
            return ApiResponse<InputAccountResponse>.Error(MessageError.OperacaoErro(OBJECT, OPERATION));
            //throw;
        }
    }
}