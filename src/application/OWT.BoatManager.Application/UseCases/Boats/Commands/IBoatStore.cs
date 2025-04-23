using OWT.BoatManager.Core.UseCases.Boats;

namespace OWT.BoatManager.Application.UseCases.Boats.Commands;
public interface IBoatStore
{
    Task<Boat?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Boat boat, CancellationToken cancellationToken = default);
    Task UpdateAsync(Boat boat, CancellationToken cancellationToken = default);
    Task<Boat?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
