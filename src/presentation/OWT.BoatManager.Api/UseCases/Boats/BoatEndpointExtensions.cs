using Microsoft.AspNetCore.Authorization;
using OWT.BoatManager.Api.Common;
using OWT.BoatManager.Api.UseCases.Boats.Dtos;
using OWT.BoatManager.Api.UseCases.Boats.Mappings;
using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.UseCases.Boats.Commands;
using OWT.BoatManager.Application.UseCases.Boats.Queries;

namespace OWT.BoatManager.Api.UseCases.Boats;

internal static class BoatEndpointExtensions
{
    public static void MapBoatEndpoints(this RouteGroupBuilder routeGroupBuilder)
    {
        var boatGroup = routeGroupBuilder.MapGroup("boats");

        boatGroup.MapGet(string.Empty,
            [Authorize(BoatScopes.Read)]
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.SendAsync(new GetBoatsQuery(), cancellationToken);
                return response.ToResult();
            });

        boatGroup.MapGet("{id:guid}",
            [Authorize(BoatScopes.Read)]
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.SendAsync(new GetBoatByIdQuery { Id = id }, cancellationToken);
                return response.ToResult();
            });

        boatGroup.MapPost(string.Empty,
            [Authorize(BoatScopes.Write)]
            async (BoatDto boatDto, ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.SendAsync(new CreateBoatCommand { Boat = boatDto.ToModel() }, cancellationToken);
                return response.ToResult();
            });

        boatGroup.MapPut("{id:guid}",
            [Authorize(BoatScopes.Write)]
            async (Guid id, BoatDto boatDto, ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.SendAsync(new UpdateBoatCommand { Boat = boatDto.ToDetailModel(id) }, cancellationToken);
                return response.ToResult();
            });

        boatGroup.MapDelete("{id:guid}",
            [Authorize(BoatScopes.Write)]
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.SendAsync(new DeleteBoatCommand { Id = id }, cancellationToken);
                return response.ToResult();
            });
    }
}
