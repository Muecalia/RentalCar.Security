using MediatR;
using RentalCar.Security.Application.Commands.Request.Users;
using RentalCar.Security.Application.Commands.Response.Users;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Application.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, ApiResponse<InputUserResponse>>
    {
        private readonly ILoggerService _loggerService;
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<InputUserResponse>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "user";
            const string OPERATION = "eliminar conta";
            try
            {
                var user = await _userRepository.Find(request.Id, cancellationToken);
                if (user == null)
                {
                    _loggerService.LogWarning(MensagemError.NotFound(OBJECT, request.Id));
                    return ApiResponse<InputUserResponse>.Error(MensagemError.NotFound(OBJECT));
                }

                await _userRepository.Delete(user, cancellationToken);
                var result = new InputUserResponse(user.Id, user.Name, user.Email, user.CreatedAt.ToShortDateString());

                _loggerService.LogInformation(MensagemError.OperacaoSucesso($"{OBJECT} {user.Name}", OPERATION));
                return ApiResponse<InputUserResponse>.Success(result, MensagemError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.OperacaoErro(OBJECT, OPERATION), ex);
                return ApiResponse<InputUserResponse>.Error(MensagemError.OperacaoErro(OBJECT, OPERATION));
                //throw;
            }
        }
    }
}
