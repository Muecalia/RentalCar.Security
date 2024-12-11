using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Commands.Response.Roles;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Roles;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleRequest, ApiResponse<InputRoleResponse>>
{
    private readonly IRoleRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public DeleteRoleHandler(IRoleRepository repository, IPrometheusService prometheusService, ILoggerService loggerService)
    {
        _repository = repository;
        _prometheusService = prometheusService;
        _loggerService = loggerService;
    }

    public async Task<ApiResponse<InputRoleResponse>> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        const string entidade = "perfil";
        const string operacao = "eliminar";
        try
        {
            var role = await _repository.GetById(request.Id, cancellationToken);
            if (role is null)
                return ApiResponse<InputRoleResponse>.Error(MessageError.NotFound(entidade));

            var result = await _repository.Delete(role);
            if (!result)
                return ApiResponse<InputRoleResponse>.Error(MessageError.OperacaoErro(entidade, operacao));

            var response = new InputRoleResponse(role.Id, role.Name);
            
            _prometheusService.AddRoleCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.OperacaoSucesso(entidade, operacao));
            return ApiResponse<InputRoleResponse>.Success(response, MessageError.OperacaoSucesso(entidade, operacao));
        }
        catch (Exception ex)
        {
            _prometheusService.AddRoleCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
            return ApiResponse<InputRoleResponse>.Error(MessageError.OperacaoErro(entidade, operacao, ex.Message));
            //throw;
        }
    }
}