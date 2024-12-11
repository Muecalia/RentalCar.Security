using Microsoft.AspNetCore.Identity;
using RentalCar.Security.Core.Entities;

namespace RentalCar.Security.Core.Repositories
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> Create(ApplicationUser applicationUser, string password, string role, CancellationToken cancellationToken);
        Task Update(ApplicationUser applicationUser, CancellationToken cancellationToken);
        Task ChangePassword(ApplicationUser applicationUser, string oldPassword, string newPassword, CancellationToken cancellationToken);
        Task Delete(ApplicationUser applicationUser, CancellationToken cancellationToken);
        Task<List<ApplicationUser>> FindAll(CancellationToken cancellationToken);
        Task<ApplicationUser?> FindById(string id, CancellationToken cancellationToken);
        Task<string> GetRoles(ApplicationUser user, CancellationToken cancellationToken);
        Task<ApplicationUser?> FindByEmail(string email, CancellationToken cancellationToken);
        Task<ApplicationUser?> FindByUser(string idUser, CancellationToken cancellationToken);
        Task<bool> IsEmailExists(string email, CancellationToken cancellationToken);
        Task<bool> IsExists(string name, CancellationToken cancellationToken);
        Task<SignInResult> SignInUser(string email, string password);
    }
}