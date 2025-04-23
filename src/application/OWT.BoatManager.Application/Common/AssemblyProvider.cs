using System.Reflection;

namespace OWT.BoatManager.Application.Common;
internal static class AssemblyProvider
{
    public static readonly Assembly Current = typeof(AssemblyProvider).Assembly;
}