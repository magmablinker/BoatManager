using OWT.BoatManager.Core.UseCases.Boats;

namespace OWT.BoatManager.Application.UseCases.Boats.Queries;
public interface IBoatQueries
{
    Task<Boat?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Boat>> GetAllAsync(CancellationToken cancellationToken = default);
}
