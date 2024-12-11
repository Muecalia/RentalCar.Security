using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Commands.Response.Account;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Account;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountRequest, ApiResponse<InputAccountResponse>>
{
    private readonly IAccountRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public UpdateAccountHandler(IAccountRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<InputAccountResponse>> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        const string entidade = "account";
        const string operacao = "atualizar conta";
        try
        {
            var account = string.IsNullOrEmpty(request.IdUser) ? await _repository.FindById(request.Id, cancellationToken) :
                await _repository.FindByUser(request.IdUser, cancellationToken);
            if (account == null)
            {
                _prometheusService.AddAccountCounter(StatusCodes.Status404NotFound.ToString());
                _loggerService.LogWarning(MessageError.NotFound(entidade, request.IdUser));
                return ApiResponse<InputAccountResponse>.Error(MessageError.NotFound(entidade));
            }

            account.Name = request.Name;
            account.Email = request.Email;
            account.PhoneNumber = request.Phone;

            await _repository.Update(account, cancellationToken);
            var result = new InputAccountResponse(account.Id, account.Name, account.Email, account.CreatedAt.ToShortDateString());
            
            _prometheusService.AddAccountCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.OperacaoSucesso($"{entidade} {account.Name}", operacao));
            return ApiResponse<InputAccountResponse>.Success(result, MessageError.OperacaoSucesso(entidade, operacao));
        }
        catch (Exception ex)
        {
            _prometheusService.AddAccountCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(entidade, operacao), ex);
            return ApiResponse<InputAccountResponse>.Error(MessageError.OperacaoErro(entidade, operacao));
            //throw;
        }
    }
}