using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Commands.Response.Account;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Account;

public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, ApiResponse<InputAccountResponse>>
    {
        private readonly IAccountRepository _repository;
        private readonly ILoggerService _loggerService;
        private readonly IPrometheusService _prometheusService;

        public CreateAccountHandler(IAccountRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
        {
            _repository = repository;
            _loggerService = loggerService;
            _prometheusService = prometheusService;
        }

        public async Task<ApiResponse<InputAccountResponse>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "account";
            const string OPERATION = "criar conta";
            try
            {
                if (await _repository.IsExists(request.Name, cancellationToken))
                {
                    _prometheusService.AddAccountCounter(StatusCodes.Status409Conflict.ToString());
                    _loggerService.LogWarning(MessageError.Conflito($"{OBJECT} {request.Name}"));
                    return ApiResponse<InputAccountResponse>.Error(MessageError.Conflito($"{OBJECT}"));
                }
                if (await _repository.IsEmailExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MessageError.ConflitoEmail(request.Email));
                    _prometheusService.AddAccountCounter(StatusCodes.Status409Conflict.ToString());
                    return ApiResponse<InputAccountResponse>.Error(MessageError.ConflitoEmail(request.Email));
                }

                var newAccount = new ApplicationUser 
                {
                    Name = request.Name,
                    Email = request.Email,
                    IdUser = request.IdUser,
                    IsClient = request.IsClient,
                    UserName = request.Email,
                    PhoneNumber = request.Phone
                };

                var account = await _repository.Create(newAccount, request.Password, request.Role, cancellationToken);

                var result = new InputAccountResponse(account.Id, account.Name, account.Email, account.CreatedAt.ToShortDateString());
                _prometheusService.AddAccountCounter(StatusCodes.Status201Created.ToString());
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