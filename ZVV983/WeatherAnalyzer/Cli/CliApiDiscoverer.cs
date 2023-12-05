using Microsoft.Extensions.Configuration;

namespace WeatherAnalyzer.Cli;

public class CliApiDiscoverer<TApi>(IConfiguration configuration)
{
    public IDictionary<string, TApi> LoadImplementations() =>
        (from impl in
                from type in typeof(CliApiDiscoverer<>).Assembly.GetTypes()
                where type is { IsPublic: true, IsAbstract: false }
                where type.IsAssignableTo(typeof(TApi)) && type.IsAssignableTo(typeof(ICliDiscoverableApi))
                where HasCompatibleConstructor(type)
                select CreateInstance(type, configuration)
            where impl is not null
            select impl).ToDictionary(impl => ((ICliDiscoverableApi)impl).CliName);

    private static bool HasCompatibleConstructor(Type type) =>
        type.GetConstructor(new[] { typeof(IConfiguration) }) is not null ||
        type.GetConstructor(Array.Empty<Type>()) is not null;

    private static TApi? CreateInstance(Type apiType, IConfiguration configuration)
    {
        var constructor = apiType.GetConstructor(new[] { typeof(IConfiguration) });
        if (constructor is not null)
        {
            return (TApi?)Activator.CreateInstance(apiType, configuration);
        }

        constructor = apiType.GetConstructor(Array.Empty<Type>());
        if (constructor is not null)
        {
            return (TApi?)Activator.CreateInstance(apiType);
        }

        return default;
    }
}
