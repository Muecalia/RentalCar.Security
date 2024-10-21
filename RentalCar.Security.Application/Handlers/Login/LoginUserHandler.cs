using MediatR;
using RentalCar.Security.Application.Commands.Request.Login;
using RentalCar.Security.Application.Commands.Response.Login;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Application.Handlers.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, ApiResponse<LoginUserResponse>>
    {
        private readonly ILoginService _loginService;
        private readonly ILoggerService _loggerService;
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(ILoggerService loggerService, IUserRepository userRepository, ILoginService loginService)
        {
            _loginService = loginService;
            _loggerService = loggerService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "user";
            const string OPERATION = "logar";
            try
            {
                var user = await _userRepository.FindByEmail(request.Email, cancellationToken);
                if (user == null)
                {
                    _loggerService.LogWarning(MensagemError.NotFoundEmail(OBJECT, request.Email));
                    return ApiResponse<LoginUserResponse>.Error(MensagemError.NotFound(OBJECT));
                }
                var role = await _userRepository.GetRoles(user, cancellationToken);                

                var resultLogin = await _userRepository.SignInUser(request.Email, request.Password);
                if (!resultLogin.Succeeded)
                {
                    _loggerService.LogWarning(MensagemError.SenhaErrada(user.Name));
                    return ApiResponse<LoginUserResponse>.Error(MensagemError.SenhaErrada());
                }

                var token = _loginService.GenerateJwtToken(user.Name, user.Email, role);
                if (token == null)
                {
                    _loggerService.LogWarning(MensagemError.NotFoundEmail(OBJECT, request.Email));
                    return ApiResponse<LoginUserResponse>.Error(MensagemError.NotFound(OBJECT));
                }
                System.Diagnostics.Debug.Print($"token: {token}");

                var result = new LoginUserResponse(user.Id, user.Name, user.Email, token, role);
                //var result = new LoginUserResponse(user.Id, user.Name, user.Email, string.Empty, role);
                return ApiResponse<LoginUserResponse>.Success(result, MensagemError.OperacaoSucesso(OBJECT, OPERATION));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.OperacaoErro(OBJECT, OPERATION), ex);
                return ApiResponse<LoginUserResponse>.Error(MensagemError.OperacaoErro(OBJECT, OPERATION));
                //throw;
            }
        }

    }
}
