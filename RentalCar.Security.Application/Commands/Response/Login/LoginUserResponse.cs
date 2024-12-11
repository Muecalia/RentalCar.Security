namespace RentalCar.Security.Application.Commands.Response.Login;

public record LoginUserResponse(string Id, string Name, string Email, string Token, string Role);