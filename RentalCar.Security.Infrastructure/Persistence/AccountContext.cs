using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalCar.Security.Core.Entities;

namespace RentalCar.Security.Infrastructure.Persistence;

public class AccountContext : IdentityDbContext<ApplicationUser>
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }
    
    //public DbSet<ApplicationUser> Entity { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>(e => 
        {
            //e.HasKey(a => a.Id);
            e.HasIndex(a => a.IdUser);
            e.HasIndex(a => a.Name).IsUnique();
            e.HasIndex(a => a.Email).IsUnique();
        });

        base.OnModelCreating(builder);
    }
    
}