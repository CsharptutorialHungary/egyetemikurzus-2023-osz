using System.CommandLine.IO;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Geocode.Api;
using WeatherAnalyzer.Geocode.Api.File;
using WeatherAnalyzer.Util;
using WeatherAnalyzer.Weather;
using WeatherAnalyzer.Weather.Api;
using WeatherAnalyzer.Weather.Api.File;

namespace WeatherAnalyzer.Tests;

public class WeatherAnalyzerProgramTests
{
    [Test, Order(1)]
    public async Task DownloadWeatherForecastsAsync_InMemory_CreatesOutputFile()
    {
        var geocodeApi = new TestGeocodeApi();
        var weatherForecastApi = new TestWeatherForecastApi();
        var program = new WeatherAnalyzerProgram(geocodeApi, weatherForecastApi, ChooseFirstCity, new TestConsole());
        var outputFile = new FileInfo($"{nameof(DownloadWeatherForecastsAsync_InMemory_CreatesOutputFile)}.json");
        outputFile.Delete();
        const string location = "Arcadia";

        await program.DownloadWeatherForecastsAsync(location, outputFile);

        Assert.That(outputFile, Has.Property(nameof(FileInfo.Exists)).True);
    }

    [Test, Order(2)]
    public async Task AnalyzeWeatherForecastAsync_LocalFile_WritesToConsoleOut()
    {
        var geocodeApi = new FileGeocodeApi();
        var weatherForecastApi = new FileWeatherForecastApi();
        var console = new TestConsole();
        var program = new WeatherAnalyzerProgram(geocodeApi, weatherForecastApi, ChooseFirstCity, console);
        const string location = $"{nameof(DownloadWeatherForecastsAsync_InMemory_CreatesOutputFile)}.json";

        await program.AnalyzeWeatherForecastAsync(location);
        var stdout = console.Out.ToString();

        Assert.That(stdout, Is.Not.Empty);
    }

    private static async Task<City?> ChooseFirstCity(IAsyncEnumerable<City> cities)
    {
        return await cities.FirstOrDefaultAsync();
    }

    private class TestGeocodeApi : IGeocodeApi
    {
        public readonly Location Location = new(4.2f, 42f);

        public async IAsyncEnumerable<City> FindCitiesAsync(string name)
        {
            yield return new City(name, "TST", "Test Environment", Location, default);
        }
    }

    private class TestWeatherForecastApi : IWeatherForecastApi
    {
        public readonly WeatherForecast WeatherForecast = new(
            DateTime.Today,
            default,
            new ValueUnit<float>(20, "°C"),
            new ValueUnit<float>(21, "°C"),
            new Precipitation(.1f, new ValueUnit<float>(10, "mm")),
            new ValueUnit<float>(30, "%"),
            new ValueUnit<float>(10, "°C"),
            WeatherCode.Overcast,
            new ValueUnit<float>(40, "%"),
            new ValueUnit<float>(25000, "m"),
            new Wind(new ValueUnit<float>(5, "km/h"), new ValueUnit<int>(42, "°"))
        );

        public async IAsyncEnumerable<WeatherForecast> GetWeatherForecastsAsync(City city)
        {
            yield return WeatherForecast with { Location = city.Location };
        }
    }
}
