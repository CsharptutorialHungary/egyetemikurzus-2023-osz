using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Geocode.Api.File;

[SuppressMessage("ReSharper", "UnusedType.Global")] // Used by reflection
public class FileGeocodeApi : IGeocodeApi, ICliDiscoverableApi
{
    public string CliName => "file";

    public async IAsyncEnumerable<City> FindCitiesAsync(string name)
    {
        var stream = System.IO.File.OpenRead(name);

        var storedCity = await JsonSerializer.DeserializeAsync<StoredCity>(stream);
        if (storedCity is null) yield break;

        yield return storedCity.City with { Name = name };
    }
}
