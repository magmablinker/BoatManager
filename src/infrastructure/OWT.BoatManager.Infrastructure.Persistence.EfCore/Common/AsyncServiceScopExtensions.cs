using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;
public static class AsyncServiceScopeExtensions
{
    public static async Task MigrateAsync(this AsyncServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<BoatManagerDbContext>();
        await context.Database.MigrateAsync();
    }
}