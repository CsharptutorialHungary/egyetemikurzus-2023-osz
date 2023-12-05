using System.CommandLine;
using System.CommandLine.IO;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using WeatherAnalyzer;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Geocode.Api;
using WeatherAnalyzer.Weather.Api;

var config = LoadConfig();
var geocodeApiDiscoverer = new CliApiDiscoverer<IGeocodeApi>(config);
var geocodeImpls = geocodeApiDiscoverer.LoadImplementations();
var weatherForecastApiDiscoverer = new CliApiDiscoverer<IWeatherForecastApi>(config);
var weatherForecastImpls = weatherForecastApiDiscoverer.LoadImplementations();

var geocodeApiOpt = new Option<string>(
        name: "--geocode-api",
        description: "The API to get coordinates from the given location"
    )
    {
        IsRequired = true
    }
    .FromAmong(geocodeImpls.Keys.ToArray());
var locationOpt = new Option<string>(
    name: "--location",
    description: "The location to retrieve weather forecast for"
)
{
    IsRequired = true
};
var weatherApiOpt = new Option<string>(
        name: "--weather-api",
        description: "The API to retrieve weather forecast from"
    )
    {
        IsRequired = true
    }
    .FromAmong(weatherForecastImpls.Keys.ToArray());
var downloadOutputFileOpt = new Option<FileInfo>(
    name: "--download",
    description: "The file to download the data to"
)
{
    IsRequired = false
};

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

static IConfiguration LoadConfig() => new ConfigurationBuilder()
    .AddJsonFile($"{Process.GetCurrentProcess().ProcessName}.json", optional: true)
    .AddJsonFile("WeatherAnalyzer.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

async Task<int> Main(string geocodeApi, string location, string weatherApi, FileInfo? downloadOutFile)
{
    try
    {
        var geocodeImpl = geocodeImpls[geocodeApi];
        var weatherForecastImpl = weatherForecastImpls[weatherApi];

        var weatherAnalyzer = new WeatherAnalyzerProgram(geocodeImpl, weatherForecastImpl, ChooseCity, new SystemConsole());

        if (downloadOutFile is not null)
        {
            await weatherAnalyzer.DownloadWeatherForecastsAsync(location, downloadOutFile);
            return 0;
        }

        await weatherAnalyzer.AnalyzeWeatherForecastAsync(location);
        return 0;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return 1;
    }
}

static async Task<City?> ChooseCity(IAsyncEnumerable<City> cityOptions)
{
    IReadOnlyList<City> cityList = await cityOptions.ToListAsync();

    switch (cityList.Count)
    {
        case 0: return null;
        case 1: return cityList[0];
    }

    for (var i = 0; i < cityList.Count; i++)
    {
        Console.WriteLine($"{i + 1}.\t{cityList[i]}");
    }

    Console.Write("?: ");

    if (int.TryParse(Console.ReadLine(), out var choice) is false) return null;
    choice--;

    if (choice >= 0 && choice < cityList.Count) return cityList[choice];
    return null;
}
