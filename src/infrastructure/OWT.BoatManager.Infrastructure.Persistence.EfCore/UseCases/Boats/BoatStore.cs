using OWT.BoatManager.Application.UseCases.Boats.Commands;
using OWT.BoatManager.Core.UseCases.Boats;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.UseCases.Boats;
internal sealed class BoatStore : IBoatStore
{
    private readonly BoatManagerDbContext _context;

    public BoatStore(BoatManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Boat?> FindAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Boats.FindAsync([id], cancellationToken);

    public async Task AddAsync(Boat boat, CancellationToken cancellationToken = default)
    {
        _context.Boats.Add(boat);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Boat boat, CancellationToken cancellationToken = default)
    {
        _context.Boats.Update(boat);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Boat?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var boat = await _context.Boats.FindAsync([id], cancellationToken);
        if (boat is null) return null;
        _context.Boats.Remove(boat);
        await _context.SaveChangesAsync(cancellationToken);
        return boat;
    }
}
