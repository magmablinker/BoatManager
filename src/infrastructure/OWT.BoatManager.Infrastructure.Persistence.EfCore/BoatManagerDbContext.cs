using Microsoft.EntityFrameworkCore;
using OWT.BoatManager.Core.UseCases.Boats;
using OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore;
internal sealed class BoatManagerDbContext(DbContextOptions<BoatManagerDbContext> options) : DbContext(options)
{
    public DbSet<Boat> Boats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyProvider.Current);
        base.OnModelCreating(modelBuilder);
    }
}
