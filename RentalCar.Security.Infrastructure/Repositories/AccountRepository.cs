using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;

namespace RentalCar.Security.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task ChangePassword(ApplicationUser applicationUser, string oldPassword, string newPassword, CancellationToken cancellationToken)
    {
        await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
    }

    public async Task<ApplicationUser> Create(ApplicationUser applicationUser, string password, string role, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(applicationUser, password);

        System.Diagnostics.Debug.Print($"result: {result.Succeeded}");
        System.Diagnostics.Debug.WriteLine(result.Succeeded);

        await _userManager.AddToRoleAsync(applicationUser, role);
        return applicationUser;
    }

    public async Task Delete(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        applicationUser.IsDeleted = true;
        applicationUser.DeletedAt = DateTime.Now;
        await _userManager.UpdateAsync(applicationUser);
    }

    public async Task<ApplicationUser?> FindById(string id, CancellationToken cancellationToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id, cancellationToken);
    }

    public async Task<List<ApplicationUser>> FindAll(CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<ApplicationUser?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && u.Email.Equals(email), cancellationToken);
    }

    public async Task<ApplicationUser?> FindByUser(string idUser, CancellationToken cancellationToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => !u.IsDeleted && u.IdUser == idUser, cancellationToken);
    }

    public async Task<string> GetRoles(ApplicationUser user, CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var item in roles)
        {
            sb.Append(item);
            if (item != roles.LastOrDefault())
                sb.Append(',');
        }
        return sb.ToString();
    }

    public async Task<bool> IsEmailExists(string email, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AnyAsync(u => string.Equals(u.Email, email), cancellationToken);
    }

    public async Task<bool> IsExists(string name, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AnyAsync(u => u.Name.Equals(name), cancellationToken);
    }

    public async Task<SignInResult> SignInUser(string email, string password)
    {
        return await _signInManager.PasswordSignInAsync(email, password, false, false);
    }

    public async Task Update(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        applicationUser.UpdatedAt = DateTime.Now;
        await _userManager.UpdateAsync(applicationUser);
    }
}