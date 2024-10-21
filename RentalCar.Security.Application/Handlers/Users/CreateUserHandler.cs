using MediatR;
using RentalCar.Security.Application.Commands.Request.Users;
using RentalCar.Security.Application.Commands.Response.Users;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Application.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, ApiResponse<InputUserResponse>>
    {
        private readonly ILoggerService _loggerService;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<InputUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "user";
            const string OPERATION = "criar conta";
            try
            {
                if (await _userRepository.IsExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MensagemError.Conflito($"{OBJECT} {request.Name}"));
                    return ApiResponse<InputUserResponse>.Error(MensagemError.Conflito($"{OBJECT}"));
                }
                if (await _userRepository.IsEmailExists(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MensagemError.ConflitoEmail(request.Email));
                    return ApiResponse<InputUserResponse>.Error(MensagemError.ConflitoEmail(request.Email));
                }

                var newUser = new ApplicationUser 
                {
                    Name = request.Name,
                    Email = request.Email,
                    IdUser = request.IdUser,
                    IsClient = request.IsClient,
                    UserName = request.Email,
                    PhoneNumber = request.Phone
                };

                var user = await _userRepository.Create(newUser, request.Password, request.Role, cancellationToken);

                var result = new InputUserResponse(user.Id, user.Name, user.Email, user.CreatedAt.ToShortDateString());

                _loggerService.LogInformation(MensagemError.OperacaoSucesso($"{OBJECT} {user.Name}", OPERATION));
                return ApiResponse<InputUserResponse>.Success(result, MensagemError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.OperacaoErro(OBJECT, OPERATION), ex.InnerException);
                return ApiResponse<InputUserResponse>.Error(MensagemError.OperacaoErro(OBJECT, OPERATION));
                //throw;
            }
        }
    }
}
