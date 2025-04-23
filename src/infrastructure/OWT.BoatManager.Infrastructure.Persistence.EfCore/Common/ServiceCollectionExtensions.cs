using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OWT.BoatManager.Application.UseCases.Boats.Commands;
using OWT.BoatManager.Application.UseCases.Boats.Queries;
using OWT.BoatManager.Infrastructure.Persistence.EfCore.UseCases.Boats;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfCorePersistence(this IServiceCollection services, Action<EfCorePersistenceOptions> configure)
    {
        var options = new EfCorePersistenceOptions();
        configure(options);
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            throw new InvalidOperationException(
                $"{nameof(EfCorePersistenceOptions)}.{nameof(EfCorePersistenceOptions.ConnectionString)} must be set");
        }

        services.AddDbContext<BoatManagerDbContext>(o => o.UseSqlServer(options.ConnectionString));

        services.AddQueries()
            .AddStores();

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services) =>
        services.AddScoped<IBoatQueries, BoatQueries>();

    private static void AddStores(this IServiceCollection services)
    {
        services.AddScoped<IBoatStore, BoatStore>();
    }
}
