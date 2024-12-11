using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Queries.Request.Roles;
using RentalCar.Security.Application.Queries.Response.Roles;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Roles;

public class FindAllRolesHandler : IRequestHandler<FindAllRolesRequest, PagedResponse<FindRoleResponse>>
{
    private readonly IRoleRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public FindAllRolesHandler(IRoleRepository repository, IPrometheusService prometheusService, ILoggerService loggerService)
    {
        _repository = repository;
        _prometheusService = prometheusService;
        _loggerService = loggerService;
    }

    public async Task<PagedResponse<FindRoleResponse>> Handle(FindAllRolesRequest request, CancellationToken cancellationToken)
    {
        const string entidade = "perfil";
        const string operacao = "Pesquisar Perfil";
        
        try
        {
            var result = new List<FindRoleResponse>();
            var roles = await _repository.GetAll(cancellationToken);

            roles.ForEach(role => result.Add(new FindRoleResponse(role.Id, role.Name)));
            
            _prometheusService.AddRoleCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.CarregamentoSucesso(entidade));
            return new PagedResponse<FindRoleResponse>(result, MessageError.CarregamentoSucesso(entidade, roles.Count));
        }
        catch (Exception ex)
        {
            _prometheusService.AddRoleCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.CarregamentoErro(entidade, ex.Message));
            return new PagedResponse<FindRoleResponse>(MessageError.CarregamentoErro(entidade));
            //throw;
        }
    }
}