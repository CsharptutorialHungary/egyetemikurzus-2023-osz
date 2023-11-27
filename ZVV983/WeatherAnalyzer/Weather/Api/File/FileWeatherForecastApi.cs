using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Weather.Api.File;

[SuppressMessage("ReSharper", "UnusedType.Global")] // Used by reflection
public class FileWeatherForecastApi : IWeatherForecastApi, ICliDiscoverableApi
{
    public string CliName => "file";

    public async IAsyncEnumerable<WeatherForecast> GetWeatherForecastsAsync(City city)
    {
        var stream = System.IO.File.OpenRead(city.Name);

        var storedForecast = await JsonSerializer.DeserializeAsync<StoredWeatherForecast>(stream);
        if (storedForecast is null) yield break;

        await foreach (var forecast in storedForecast.WeatherForecasts)
        {
            yield return forecast;
        }
    }
}
