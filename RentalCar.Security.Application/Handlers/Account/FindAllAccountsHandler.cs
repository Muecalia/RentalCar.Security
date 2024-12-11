using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Queries.Request.Account;
using RentalCar.Security.Application.Queries.Response.Account;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Account;

public class FindAllAccountsHandler : IRequestHandler<FindAllAccountsRequest, PagedResponse<FindAllAccountsResponse>>
{
    private readonly IAccountRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public FindAllAccountsHandler(IAccountRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<PagedResponse<FindAllAccountsResponse>> Handle(FindAllAccountsRequest request, CancellationToken cancellationToken)
    {
        const string OBJECT = "account";
        try
        {
            var results = new List<FindAllAccountsResponse>();
            var accounts = await _repository.FindAll(cancellationToken);

            results = accounts.Select(u => new FindAllAccountsResponse(u.Id, u.Name, u.Email, u.PhoneNumber, u.IsClient ? "Cliente" : "Admin")).ToList();

            _prometheusService.AddAccountCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.CarregamentoSucesso(OBJECT, accounts.Count));
            return new PagedResponse<FindAllAccountsResponse>(results, MessageError.CarregamentoSucesso(OBJECT));
        }
        catch (Exception ex)
        {
            _prometheusService.AddAccountCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.CarregamentoErro(OBJECT), ex);
            return new PagedResponse<FindAllAccountsResponse>(MessageError.CarregamentoErro(OBJECT));
            //throw;
        }
    }
}