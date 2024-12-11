using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCar.Security.Core.Repositories;

namespace RentalCar.Security.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityRole> Create(string Name)
    {
        var role = new IdentityRole(Name);
        await _roleManager.CreateAsync(role);
        return role;
    }

    public async Task<bool> Delete(IdentityRole role)
    {
        var result = await _roleManager.DeleteAsync(role);
        return result.Succeeded;
    }

    public async Task<bool> Exists(string role, CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.AnyAsync(r => string.Equals(r.Name, role), cancellationToken);
    }

    public async Task<List<IdentityRole>> GetAll(CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.ToListAsync(cancellationToken);
    }

    public async Task<IdentityRole?> GetById(string Id, CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.FirstOrDefaultAsync(r => string.Equals(r.Id, Id), cancellationToken);
    }

    public async Task<bool> Update(IdentityRole Role)
    {
        var result = await _roleManager.UpdateAsync(Role);
        return result.Succeeded;
    }
    
}