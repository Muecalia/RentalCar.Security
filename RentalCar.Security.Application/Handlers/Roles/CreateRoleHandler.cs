using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Commands.Response.Roles;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;
using Serilog;

namespace RentalCar.Security.Application.Handlers.Roles;

public class CreateRoleHandler : IRequestHandler<CreateRoleRequest, ApiResponse<InputRoleResponse>>
{
    private readonly IRoleRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public CreateRoleHandler(IRoleRepository repository, IPrometheusService prometheusService, ILoggerService loggerService)
    {
        _repository = repository;
        _prometheusService = prometheusService;
        _loggerService = loggerService;
    }

    public async Task<ApiResponse<InputRoleResponse>> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        const string entidade = "perfil";
        const string operacao = "criar"; 
        try
        {
            if (await _repository.Exists(request.Name, cancellationToken))
            {
                Log.Warning(MessageError.Conflito($"{entidade} {request.Name}"));
                return ApiResponse<InputRoleResponse>.Error(MessageError.Conflito(entidade));
            }

            var result = await _repository.Create(request.Name);

            var response = new InputRoleResponse(result.Id, result.Name);
            //var response = new InputRoleResponse("123", "Name");

            _prometheusService.AddRoleCounter(StatusCodes.Status201Created.ToString());
            _loggerService.LogInformation(MessageError.OperacaoSucesso(entidade, operacao));
            return ApiResponse<InputRoleResponse>.Success(response, MessageError.OperacaoSucesso(entidade, operacao));
        }
        catch (Exception ex)
        {
            _prometheusService.AddRoleCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
            return ApiResponse<InputRoleResponse>.Error(MessageError.OperacaoErro(entidade, operacao));
            //throw;
        }
    }
}