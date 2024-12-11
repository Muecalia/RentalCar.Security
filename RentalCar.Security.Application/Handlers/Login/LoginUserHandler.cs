using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Security.Application.Commands.Request.Login;
using RentalCar.Security.Application.Commands.Response.Login;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;
using RentalCar.Security.Core.Wrappers;

namespace RentalCar.Security.Application.Handlers.Login;

public class LoginUserHandler : IRequestHandler<LoginUserRequest, ApiResponse<LoginUserResponse>>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILoggerService _loggerService;
        private readonly IAccountRepository _accountRepository;
        private readonly IPrometheusService _prometheusService;

        public LoginUserHandler(IJwtTokenService jwtTokenService, ILoggerService loggerService, IAccountRepository accountRepository, IPrometheusService prometheusService)
        {
            _jwtTokenService = jwtTokenService;
            _loggerService = loggerService;
            _accountRepository = accountRepository;
            _prometheusService = prometheusService;
        }

        public async Task<ApiResponse<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            const string entidade = "user";
            const string operacao = "logar";
            try
            {
                var user = await _accountRepository.FindByEmail(request.Email, cancellationToken);
                if (user == null)
                {
                    _prometheusService.AddLoginCounter(StatusCodes.Status404NotFound.ToString());
                    _loggerService.LogWarning(MessageError.NotFoundEmail(entidade, request.Email));
                    return ApiResponse<LoginUserResponse>.Error(MessageError.NotFound(entidade));
                }

                var role = await _accountRepository.GetRoles(user, cancellationToken);
                
                var resultLogin = await _accountRepository.SignInUser(request.Email, request.Password);
                if (!resultLogin.Succeeded)
                {
                    _prometheusService.AddLoginCounter(StatusCodes.Status404NotFound.ToString());
                    _loggerService.LogWarning(MessageError.SenhaErrada(user.Name));
                    return ApiResponse<LoginUserResponse>.Error(MessageError.SenhaErrada());
                }

                var token = _jwtTokenService.GenerateJwtToken(user.Name, user.Email, role);
                if (token is null)
                {
                    _prometheusService.AddLoginCounter(StatusCodes.Status404NotFound.ToString());
                    _loggerService.LogWarning(MessageError.NotFoundEmail("token", request.Email));
                    return ApiResponse<LoginUserResponse>.Error(MessageError.NotFound("token"));
                }

                var result = new LoginUserResponse(user.Id, user.Name, user.Email, token, role);
                
                _prometheusService.AddLoginCounter(StatusCodes.Status200OK.ToString());
                _loggerService.LogInformation(MessageError.OperacaoSucesso($"{entidade} {request.Email}", operacao));
                return ApiResponse<LoginUserResponse>.Success(result, MessageError.OperacaoSucesso(entidade, operacao));
            }
            catch (Exception ex)
            {
                _prometheusService.AddLoginCounter(StatusCodes.Status400BadRequest.ToString());
                _loggerService.LogError(MessageError.OperacaoErro(entidade, operacao, ex.Message));
                return ApiResponse<LoginUserResponse>.Error(MessageError.OperacaoErro(entidade, operacao));
                //throw;
            }
        }
}