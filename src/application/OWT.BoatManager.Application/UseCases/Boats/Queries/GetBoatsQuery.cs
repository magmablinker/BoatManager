using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.Common;
using OWT.BoatManager.Application.UseCases.Boats.Models;
using OWT.BoatManager.Application.UseCases.Boats.Queries.Mappings;

namespace OWT.BoatManager.Application.UseCases.Boats.Queries;

public sealed class GetBoatsQuery : IRequest<IReadOnlyList<BoatDetailModel>>;

internal sealed class GetBoatsQueryHandler : RequestHandler<GetBoatsQuery, IReadOnlyList<BoatDetailModel>>
{
    private readonly IBoatQueries _boatQueries;

    public GetBoatsQueryHandler(IBoatQueries boatQueries)
    {
        _boatQueries = boatQueries;
    }

    public override async Task<Response<IReadOnlyList<BoatDetailModel>>> HandleAsync(GetBoatsQuery request, CancellationToken cancellationToken = default)
    {
        var boats = await _boatQueries.GetAllAsync(cancellationToken);
        return Response<IReadOnlyList<BoatDetailModel>>.Success(boats.ToDetailModels());
    }
}
