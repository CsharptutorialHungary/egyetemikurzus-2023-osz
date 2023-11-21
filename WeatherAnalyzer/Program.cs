using System.CommandLine;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Geocode.Api;
using WeatherAnalyzer.Weather;
using WeatherAnalyzer.Weather.Api;

var config = LoadConfig();
var geocodeApiDiscoverer = new CliApiDiscoverer<IGeocodeApi>(config);
var geocodeImpls = geocodeApiDiscoverer.LoadImplementations();
var weatherForecastApiDiscoverer = new CliApiDiscoverer<IWeatherForecastApi>(config);
var weatherForecastImpls = weatherForecastApiDiscoverer.LoadImplementations();

var geocodeApiOpt = new Option<string>(
    name: "--geocode-api",
    description: ""
) { IsRequired = true }.FromAmong(geocodeImpls.Keys.ToArray());
var locationOpt = new Option<string>(
    name: "--location",
    description: ""
) { IsRequired = true };
var weatherApiOpt = new Option<string>(
    name: "--weather-api",
    description: ""
) { IsRequired = true }.FromAmong(weatherForecastImpls.Keys.ToArray());
var downloadOutputFileOpt = new Option<FileInfo>(name: "--download", description: "The file to download the data to")
    { IsRequired = false };

var cmd = new RootCommand
{
    geocodeApiOpt,
    locationOpt,
    weatherApiOpt,
    downloadOutputFileOpt
};
cmd.SetHandler(
    Main,
    geocodeApiOpt,
    locationOpt,
    weatherApiOpt,
    downloadOutputFileOpt
);

return await cmd.InvokeAsync(args);

IConfiguration LoadConfig() => new ConfigurationBuilder()
    .AddJsonFile($"{Process.GetCurrentProcess().ProcessName}.json", optional: true)
    .AddJsonFile("WeatherAnalyzer.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

async Task<int> Main(string geocodeApi, string location, string weatherApi, FileInfo? downloadOutFile)
{
    try
    {
        var forecasts = LoadWeatherForecastsAsync(geocodeApi, location, weatherApi);

        if (downloadOutFile is not null)
        {
            await using var file = downloadOutFile.OpenWrite();
            await JsonSerializer.SerializeAsync(file, forecasts);

            return 0;
        }

        await Analyze(forecasts);
        return 0;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return 1;
    }
}

async IAsyncEnumerable<WeatherForecast> LoadWeatherForecastsAsync(string geocodeApi, string location, string weatherApi)
{
    var geocodeImpl = geocodeImpls[geocodeApi];
    var cities = geocodeImpl.FindCitiesAsync(location);

    var city = await ChooseCity(cities);
    if (city is null)
    {
        throw new Exception("No city was found");
    }

    var weatherForecastImpl = weatherForecastImpls[weatherApi];
    var forecasts = weatherForecastImpl.GetWeatherForecastsAsync(city);

    //return forecasts;
    await foreach (var forecast in forecasts)
    {
        yield return forecast;
    }
}

async Task<City?> ChooseCity(IAsyncEnumerable<City> cityOptions)
{
    IReadOnlyList<City> cityList = await cityOptions.ToListAsync();

    if (cityList.Count == 1) return cityList[0];

    for (var i = 0; i < cityList.Count; i++)
    {
        Console.WriteLine($"{i}.\t{cityList[i]}");
    }

    Console.Write("?: ");

    if (int.TryParse(Console.ReadLine(), out var choice) is false) return null;

    if (choice >= 0 && choice < cityList.Count) return cityList[choice];
    return null;
}

async Task Analyze(IAsyncEnumerable<WeatherForecast> weatherForecasts)
{
}
