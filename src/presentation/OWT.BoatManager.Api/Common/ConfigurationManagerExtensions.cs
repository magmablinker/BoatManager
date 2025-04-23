namespace OWT.BoatManager.Api.Common;

internal static class ConfigurationManagerExtensions
{
    public static TValue GetRequiredValue<TValue>(this IConfigurationManager configurationManager, string key)
    {
        return configurationManager.GetValue<TValue>(key) ??
               throw new InvalidOperationException($"Key '{key}' was not found in configuration");
    }
}