namespace RentalCar.Security.Core.Services
{
    public interface ILoginService
    {
        string GenerateJwtToken(string name, string email, string role);
    }
}
