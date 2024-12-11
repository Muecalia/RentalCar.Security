using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Queries.Request.Roles;
using RentalCar.Security.Application.Queries.Response.Roles;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Roles;

public class FindRoleByIdHandler : IRequestHandler<FindRoleByIdRequest, ApiResponse<FindRoleResponse>>
{
    private readonly IRoleRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public FindRoleByIdHandler(IRoleRepository repository, IPrometheusService prometheusService, ILoggerService loggerService)
    {
        _repository = repository;
        _prometheusService = prometheusService;
        _loggerService = loggerService;
    }

    public async Task<ApiResponse<FindRoleResponse>> Handle(FindRoleByIdRequest request, CancellationToken cancellationToken)
    {
        const string entidade = "perfil";
        const string operacao = "Pesquisar Perfil";
        
        try
        {
            var role = await _repository.GetById(request.Id, cancellationToken);
            if (role == null)
                return ApiResponse<FindRoleResponse>.Error(MessageError.NotFound(entidade));

            var result = new FindRoleResponse(role.Id, role.Name);
            _prometheusService.AddRoleCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.CarregamentoSucesso(entidade));
            return ApiResponse<FindRoleResponse>.Success(result, MessageError.CarregamentoSucesso(entidade));
        }
        catch (Exception ex)
        {
            _prometheusService.AddRoleCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.CarregamentoErro(entidade, ex.Message));
            return ApiResponse<FindRoleResponse>.Error(MessageError.CarregamentoErro(entidade));
            //throw;
        }
    }
}