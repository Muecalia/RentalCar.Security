using MediatR;
using Microsoft.AspNetCore.Authorization;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Queries.Request.Roles;

namespace RentalCar.Security.API.Endpoints;

public static class RoleEndPoints
{
    public static void MapRoleEndPoints(this IEndpointRouteBuilder route)
    {
        route.MapGet("/role", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new FindAllRolesRequest(), cancellationToken);
            return Results.Ok(result);
        });

        route.MapGet("/role/{id}", [Authorize(Roles = "Admin")] async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new FindRoleByIdRequest(id), cancellationToken);
            return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
        });

        route.MapPost("/role", [Authorize(Roles = "Admin")] async (CreateRoleRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
        });

        route.MapDelete("/role/{id}", [Authorize(Roles = "Admin")] async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteRoleRequest(id), cancellationToken);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Message);
        });
        
        route.MapPut("/role/{id}", [Authorize(Roles = "Admin")] async (string id, UpdateRoleRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var result = await mediator.Send(request, cancellationToken);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Message);
        });

    }
    
}