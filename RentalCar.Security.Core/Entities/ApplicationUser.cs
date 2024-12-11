using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RentalCar.Security.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        IsActive = true;
        IsDeleted = false;
        EmailConfirmed = true;
        CreatedAt = DateTime.Now;
    }

    [Required, MaxLength(100)]
    public string Name { get; set; }
    public bool IsClient { get; set; }
    public string IdUser { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}