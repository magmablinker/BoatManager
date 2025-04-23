using System.Reflection;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;
internal static class AssemblyProvider
{
    public static readonly Assembly Current = typeof(AssemblyProvider).Assembly;
}
