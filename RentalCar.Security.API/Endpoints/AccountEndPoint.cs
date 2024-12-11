using MediatR;
using Microsoft.AspNetCore.Authorization;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Queries.Request.Account;

namespace RentalCar.Security.API.Endpoints;

public static class AccountEndPoint
{
    public static void MapAccountEndPoint(this IEndpointRouteBuilder route)
    {
        var accountGroup = "Account";
        var roleGroup = "Product Operations";
        
        //route.MapGet("account", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken) =>
        route.MapGet("account", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new FindAllAccountsRequest(), cancellationToken);
            return Results.Ok(result);
        });

        route.MapGet("/account/{id}", [Authorize(Roles = "Admin")] async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new FindAccountByIdRequest(id), cancellationToken);
            return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
        });

        route.MapPost("/account", [Authorize(Roles = "Admin")] async (CreateAccountRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
        });
        
        route.MapDelete("/account/{id}", [Authorize(Roles = "Admin")] async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteAccountRequest(id), cancellationToken);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
        });
        
        route.MapPut("/account/{id}", [Authorize(Roles = "Admin")] async (string id, UpdateAccountRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var result = await mediator.Send(request, cancellationToken);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
        });

        route.MapPut("/account/change-password/{id}", [Authorize(Roles = "Admin")] async (string id, ChangePasswordRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var result = await mediator.Send(request, cancellationToken);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
        });
    }
}