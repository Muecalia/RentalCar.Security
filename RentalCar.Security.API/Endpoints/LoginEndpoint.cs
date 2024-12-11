using MediatR;
using RentalCar.Security.Application.Commands.Request.Login;

namespace RentalCar.Security.API.Endpoints;

public static class LoginEndpoint
{
    public static void MapLoginEndPoint(this IEndpointRouteBuilder route)
    {
        route.MapPost("/login", async (LoginUserRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
        });
    }
}