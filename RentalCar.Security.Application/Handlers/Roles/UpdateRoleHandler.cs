using MediatR;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Commands.Response.Roles;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;

namespace RentalCar.Security.Application.Handlers.Roles
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleRequest, ApiResponse<InputRoleResponse>>
    {
        private readonly IRoleRepository _iRoleRepository;

        public UpdateRoleHandler(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }

        public async Task<ApiResponse<InputRoleResponse>> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            const string Entidade = "perfil";
            const string Operacao = "atualizar";
            try
            {
                var role = await _iRoleRepository.GetById(request.Id, cancellationToken);
                if (role == null)
                    return ApiResponse<InputRoleResponse>.Error(MensagemError.NotFound(Entidade));

                role.Name = request.Name;
                var result = await _iRoleRepository.Update(role);
                if (!result)
                    return ApiResponse<InputRoleResponse>.Error(MensagemError.OperacaoErro(Entidade, Operacao));

                var response = new InputRoleResponse(role.Id, role.Name);

                return ApiResponse<InputRoleResponse>.Success(response, MensagemError.OperacaoSucesso(Entidade, Operacao));
            }
            catch (Exception ex)
            {
                return ApiResponse<InputRoleResponse>.Error(MensagemError.OperacaoErro(Entidade, Operacao, ex.Message));
                throw;
            }
        }
    }
}
