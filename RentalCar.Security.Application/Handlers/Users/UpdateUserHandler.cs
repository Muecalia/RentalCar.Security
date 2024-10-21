using MediatR;
using RentalCar.Security.Application.Commands.Request.Users;
using RentalCar.Security.Application.Commands.Response.Users;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Application.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, ApiResponse<InputUserResponse>>
    {
        private readonly ILoggerService _loggerService;
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<InputUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "user";
            const string OPERATION = "atualizar conta";
            try
            {
                var user = await _userRepository.Find(request.Id, cancellationToken);
                if (user == null)
                {
                    _loggerService.LogWarning(MensagemError.NotFound(OBJECT, request.Id));
                    return ApiResponse<InputUserResponse>.Error(MensagemError.NotFound(OBJECT));
                }

                user.Name = request.Name;
                user.Email = request.Email;
                user.PhoneNumber = request.Phone;

                await _userRepository.Update(user, cancellationToken);
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
