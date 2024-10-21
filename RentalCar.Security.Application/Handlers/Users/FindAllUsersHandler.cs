using MediatR;
using RentalCar.Security.Application.Queries.Request.Users;
using RentalCar.Security.Application.Queries.Response.Users;
using RentalCar.Security.Application.Utils;
using RentalCar.Security.Application.Wrappers;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Application.Handlers.Users
{
    public class FindAllUsersHandler : IRequestHandler<FindAllUsersRequest, PagedResponse<FindAllUsersResponse>>
    {
        private readonly ILoggerService _loggerService;
        private readonly IUserRepository _userRepository;

        public FindAllUsersHandler(ILoggerService loggerService, IUserRepository userRepository)
        {
            _loggerService = loggerService;
            _userRepository = userRepository;
        }

        public async Task<PagedResponse<FindAllUsersResponse>> Handle(FindAllUsersRequest request, CancellationToken cancellationToken)
        {
            const string OBJECT = "user";
            try
            {
                var results = new List<FindAllUsersResponse>();
                var users = await _userRepository.FindAll(cancellationToken);

                results = users.Select(u => new FindAllUsersResponse(u.Id, u.Name, u.Email, u.PhoneNumber, u.IsActive ? "Cliente" : "Admin")).ToList();

                _loggerService.LogInformation(MensagemError.CarregamentoSucesso(OBJECT, users.Count));
                return new PagedResponse<FindAllUsersResponse>(results, MensagemError.CarregamentoSucesso(OBJECT));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MensagemError.CarregamentoErro(OBJECT), ex);
                return new PagedResponse<FindAllUsersResponse>(MensagemError.CarregamentoErro(OBJECT));
                //throw;
            }
        }
    }
}
