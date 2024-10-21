namespace RentalCar.Security.Application.Queries.Response.Users
{
    public record FindUserResponse(string Id, string Name, string Email, string Phone, string Role)
    {
    }
}
