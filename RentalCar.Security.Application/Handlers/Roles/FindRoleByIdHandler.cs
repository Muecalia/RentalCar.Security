using MediatR;
using RentalCar.Security.Application.Queries.Request.Roles;
using RentalCar.Security.Application.Queries.Response.Roles;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;

namespace RentalCar.Security.Application.Handlers.Roles
{
    public class FindRoleByIdHandler : IRequestHandler<FindRoleByIdRequest, ApiResponse<FindRoleResponse>>
    {
        private readonly IRoleRepository _iRoleRepository;

        public FindRoleByIdHandler(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }

        public async Task<ApiResponse<FindRoleResponse>> Handle(FindRoleByIdRequest request, CancellationToken cancellationToken)
        {
            const string Entidade = "perfil";
            try
            {
                var role = await _iRoleRepository.GetById(request.Id, cancellationToken);
                if (role == null)
                    return ApiResponse<FindRoleResponse>.Error(MensagemError.NotFound(Entidade));

                var result = new FindRoleResponse(role.Id, role.Name);
                return ApiResponse<FindRoleResponse>.Success(result, MensagemError.CarregamentoSucesso(Entidade));
            }
            catch (Exception ex)
            {
                return new PagedResponse<FindRoleResponse>(MensagemError.CarregamentoErro(Entidade, ex.Message));
                //throw;
            }
        }
    }
}
