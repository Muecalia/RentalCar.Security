using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Queries.Request.Account;
using RentalCar.Security.Application.Queries.Response.Account;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Account;

public class FindAccountByIdHandler : IRequestHandler<FindAccountByIdRequest, ApiResponse<FindAccountResponse>>
{
    private readonly IAccountRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public FindAccountByIdHandler(IAccountRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<FindAccountResponse>> Handle(FindAccountByIdRequest request, CancellationToken cancellationToken)
    {
        const string OBJECT = "account";
        const string operacao = "atualizar conta";
        
        try
        {
            var account = await _repository.FindById(request.Id, cancellationToken);
            if (account == null) 
            {
                _prometheusService.AddAccountCounter(StatusCodes.Status404NotFound.ToString());
                _loggerService.LogWarning(MessageError.NotFound(OBJECT, request.Id));
                return ApiResponse<FindAccountResponse>.Error(MessageError.NotFound(OBJECT));
            }

            var roles = await _repository.GetRoles(account, cancellationToken);
            var result = new FindAccountResponse(account.Id, account.Name, account.Email, account.PhoneNumber, roles);

            _prometheusService.AddAccountCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.CarregamentoSucesso($"{OBJECT} {account.Name}"));
            return ApiResponse<FindAccountResponse>.Success(result, MessageError.CarregamentoSucesso(OBJECT));
        }
        catch (Exception ex)
        {
            _prometheusService.AddAccountCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.CarregamentoErro(OBJECT), ex);
            return ApiResponse<FindAccountResponse>.Error(MessageError.CarregamentoErro(OBJECT));
            //throw;
        }
    }
}