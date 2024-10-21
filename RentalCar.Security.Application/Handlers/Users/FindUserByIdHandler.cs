using MediatR;
using RentalCar.Security.Application.Queries.Request.Users;
using RentalCar.Security.Application.Queries.Response.Users;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Application.Handlers.Users
{
    public class FindUserByIdHandler : IRequestHandler<FindUserByIdRequest, ApiResponse<FindUserResponse>>
    {
        private readonly ILoggerService _loggerService;
        private readonly IUserRepository _userRepository;

        public FindUserByIdHandler(ILoggerService loggerService, IUserRepository userRepository)
        {
            _loggerService = loggerService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<FindUserResponse>> Handle(FindUserByIdRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "user";
            try
            {
                var user = await _userRepository.Find(request.Id, cancellationToken);
                if (user == null) 
                {
                    _loggerService.LogWarning(MensagemError.NotFound(OBJECT, request.Id));
                    return ApiResponse<FindUserResponse>.Error(MensagemError.NotFound(OBJECT));
                }

                var roles = await _userRepository.GetRoles(user, cancellationToken);
                var result = new FindUserResponse(user.Id, user.Name, user.Email, user.PhoneNumber, roles);

                _loggerService.LogInformation(MensagemError.CarregamentoSucesso($"{OBJECT} {user.Name}"));
                return ApiResponse<FindUserResponse>.Success(result, MensagemError.CarregamentoSucesso(OBJECT));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.CarregamentoErro(OBJECT), ex);
                return ApiResponse<FindUserResponse>.Error(MensagemError.CarregamentoErro(OBJECT));
                //throw;
            }
        }
    }
}
