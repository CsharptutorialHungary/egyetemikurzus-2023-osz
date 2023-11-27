using System.Diagnostics.CodeAnalysis;
using WeatherAnalyzer.Cli;

namespace WeatherAnalyzer.Geocode.Api.Gps;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")] // Instantiated by reflection
public class GpsGeocodeApi : IGeocodeApi, ICliDiscoverableApi
{
    public string CliName => "gps";

    public async IAsyncEnumerable<City> FindCitiesAsync(string name)
    {
        var parts = name.Split(':');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid GPS format. Please specify it as LAT:LON");
        }

        var lat = float.Parse(parts[0]);
        var lon = float.Parse(parts[1]);

        yield return new City(
            "", 
            "",
            "",
            new Location(lat, lon),
            0
        );
    }
}
