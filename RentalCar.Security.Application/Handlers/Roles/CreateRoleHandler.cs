using MediatR;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Commands.Response.Roles;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;
using Serilog;

namespace RentalCar.Security.Application.Handlers.Roles
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleRequest, ApiResponse<InputRoleResponse>>
    {
        private readonly IRoleRepository _iRoleRepository;

        public CreateRoleHandler(IRoleRepository iRoleRepository)
        {
            _iRoleRepository = iRoleRepository;
        }

        public async Task<ApiResponse<InputRoleResponse>> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            const string Entidade = "perfil";
            const string Operacao = "criar"; 
            try
            {
                if (await _iRoleRepository.Exists(request.Name, cancellationToken))
                {
                    Log.Warning(MensagemError.Conflito($"{Entidade} {request.Name}"));
                    return ApiResponse<InputRoleResponse>.Error(MensagemError.Conflito(Entidade));
                }

                var result = await _iRoleRepository.Create(request.Name);

                var rsponse = new InputRoleResponse(result.Id, result.Name);

                Log.Information(MensagemError.OperacaoSucesso(Entidade, Operacao));
                return ApiResponse<InputRoleResponse>.Success(rsponse, MensagemError.OperacaoSucesso(Entidade, Operacao));
            }
            catch (Exception ex)
            {
                Log.Error(MensagemError.OperacaoErro(Entidade, Operacao, ex.Message));
                return ApiResponse<InputRoleResponse>.Error(MensagemError.OperacaoErro(Entidade, Operacao));
                throw;
            }
        }
    }
}
