using Microsoft.Extensions.DependencyInjection;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;
public static class HealthChecksBuilderExtensions
{
    public static void AddDatabaseChecks(this IHealthChecksBuilder builder) =>
        builder.AddDbContextCheck<BoatManagerDbContext>();
}