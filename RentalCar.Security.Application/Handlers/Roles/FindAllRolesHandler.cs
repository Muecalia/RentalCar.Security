using MediatR;
using RentalCar.Security.Application.Queries.Request.Roles;
using RentalCar.Security.Application.Queries.Response.Roles;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;

namespace RentalCar.Security.Application.Handlers.Roles
{
    public class FindAllRolesHandler : IRequestHandler<FindAllRolesRequest, PagedResponse<FindRoleResponse>>
    {
        private readonly IRoleRepository _iRoleRepository;

        public FindAllRolesHandler(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }

        public async Task<PagedResponse<FindRoleResponse>> Handle(FindAllRolesRequest request, CancellationToken cancellationToken)
        {
            const string Entidade = "perfil";
            try
            {
                var result = new List<FindRoleResponse>();
                var roles = await _iRoleRepository.GetAll(cancellationToken);

                roles.ForEach(role => result.Add(new FindRoleResponse(role.Id, role.Name)));

                return new PagedResponse<FindRoleResponse>(result, MensagemError.CarregamentoSucesso(Entidade, roles.Count));
            }
            catch (Exception ex)
            {
                return new PagedResponse<FindRoleResponse>(MensagemError.CarregamentoErro(Entidade, ex.Message));
                //throw;
            }
        }
    }
}
