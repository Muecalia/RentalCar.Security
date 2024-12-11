namespace RentalCar.Security.Application.Queries.Response.Account;

public record FindAccountResponse(string Id, string Name, string Email, string Phone, string Role);