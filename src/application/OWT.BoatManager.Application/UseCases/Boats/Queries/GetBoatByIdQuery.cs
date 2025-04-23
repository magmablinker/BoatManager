using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.Common;
using OWT.BoatManager.Application.UseCases.Boats.Models;
using OWT.BoatManager.Application.UseCases.Boats.Queries.Mappings;

namespace OWT.BoatManager.Application.UseCases.Boats.Queries;
public sealed class GetBoatByIdQuery : IRequest<BoatDetailModel>
{
    public required Guid Id { get; init; }
}

internal sealed class GetBoatByIdQueryHandler : RequestHandler<GetBoatByIdQuery, BoatDetailModel>
{
    private readonly IBoatQueries _boatQueries;

    public GetBoatByIdQueryHandler(IBoatQueries boatQueries)
    {
        _boatQueries = boatQueries;
    }

    public override async Task<Response<BoatDetailModel>> HandleAsync(GetBoatByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var boat = await _boatQueries.FindAsync(request.Id, cancellationToken);
        return boat is null
            ? Response<BoatDetailModel>.Failure(ErrorDefaults.Generic.NotFound())
            : Response<BoatDetailModel>.Success(boat.ToDetailModel());
    }
}