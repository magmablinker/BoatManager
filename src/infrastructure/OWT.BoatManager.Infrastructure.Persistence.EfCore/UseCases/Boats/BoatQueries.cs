using Microsoft.EntityFrameworkCore;
using OWT.BoatManager.Application.UseCases.Boats.Queries;
using OWT.BoatManager.Core.UseCases.Boats;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.UseCases.Boats;
internal sealed class BoatQueries : IBoatQueries
{
    private readonly BoatManagerDbContext _context;

    public BoatQueries(BoatManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Boat?> FindAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Boats.AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Boat>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Boats.AsNoTracking()
            .ToListAsync(cancellationToken);
}
