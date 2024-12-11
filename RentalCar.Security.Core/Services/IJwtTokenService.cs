namespace RentalCar.Security.Core.Services;

public interface IJwtTokenService
{
    string GenerateJwtToken(string name, string email, string role);
}