namespace RentalCar.Security.Application.Queries.Response.Account;

public record FindAllAccountsResponse(string Id, string Name, string Email, string Phone, string TypeUser);