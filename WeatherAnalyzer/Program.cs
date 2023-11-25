using System.CommandLine;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Geocode.Api;
using WeatherAnalyzer.Util;
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
    var allForecasts = await weatherForecasts.ToListAsync();

    AnalyzeWeatherForecasts(allForecasts);
    Console.WriteLine();

    var byDay = from forecast in allForecasts
        group forecast by DateOnly.FromDateTime(forecast.Time);

    foreach (var day in byDay)
    {
        AnalyzeWeatherForecasts(day.ToList());
        Console.WriteLine();
    }

    return;

    void AnalyzeWeatherForecasts(IList<WeatherForecast> forecasts)
    {
        var times = (from forecast in forecasts
            select DateOnly.FromDateTime(forecast.Time)).ToList();

        var timeMin = times.Min();
        var timeMax = times.Max();
        Console.WriteLine(timeMin == timeMax ? $"{timeMin}:" : $"{timeMin}-{timeMax}:");

        const char tab = '\t';

        Console.Write(tab);
        Console.WriteLine(CreateAnalysis(forecasts, "Temperature", f => f.Temperature));

        Console.Write(tab);
        Console.WriteLine(CreateAnalysis(forecasts, "Apparent temperature", f => f.ApparentTemperature));

        var precipitationAnalysis = CreateAnalysis(forecasts, "Precipitation amount", f => f.Precipitation.Amount);
        var precipitationProbabilityMin = forecasts.Min(f => f.Precipitation.Probability);
        var precipitationProbabilityMax = forecasts.Max(f => f.Precipitation.Probability);
        precipitationAnalysis =
            $"{precipitationAnalysis}, probability: {precipitationProbabilityMin:P1}..{precipitationProbabilityMax:P1}";
        Console.Write(tab);
        Console.WriteLine(precipitationAnalysis);

        var humidityMin = forecasts.Min(f => f.Humidity);
        var humidityMax = forecasts.Max(f => f.Humidity);
        var humidityAvg = forecasts.Average(f => f.Humidity);
        Console.Write(tab);
        Console.WriteLine($"Humidity: {humidityMin:P1}..{humidityMax:P1}, average: {humidityAvg:P1}");

        Console.Write(tab);
        Console.WriteLine(CreateAnalysis(forecasts, "Dew point", f => f.DewPoint));

        Console.Write(tab);
        Console.WriteLine(CreateAnalysis(forecasts, "Wind", f => f.Wind.Speed));
    }

    string CreateAnalysis(
        IList<WeatherForecast> forecasts,
        string by,
        Func<WeatherForecast, ValueUnit<float>> selector
    )
    {
        return CreateAnalysis2(forecasts, by, f => selector(f).Value, f => selector(f).Unit);
    }

    string CreateAnalysis2(
        IList<WeatherForecast> forecasts,
        string by,
        Func<WeatherForecast, float> valueSelector,
        Func<WeatherForecast, string> unitSelector
    )
    {
        var (min, max, avg) = AnalyzeWeatherForecastsBy(forecasts, valueSelector);
        var minValue = valueSelector(min);
        var maxValue = valueSelector(max);
        var unit = unitSelector(min);

        return $"{by}: {minValue:F1}{unit}..{maxValue:F1}{unit}, average: {avg:F1}{unit}";
    }

    (WeatherForecast Min, WeatherForecast Max, float Average)
        AnalyzeWeatherForecastsBy(IList<WeatherForecast> forecasts, Func<WeatherForecast, float> selector)
    {
        var min = forecasts.MinBy(selector);
        var max = forecasts.MaxBy(selector);
        if (min is null || max is null)
        {
            throw new ArgumentException("List was empty", nameof(forecasts));
        }

        return (min, max, forecasts.Average(selector));
    }
}