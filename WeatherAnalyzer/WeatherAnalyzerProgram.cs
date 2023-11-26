using System.CommandLine;
using System.CommandLine.IO;
using System.Text.Json;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Geocode.Api;
using WeatherAnalyzer.Util;
using WeatherAnalyzer.Weather;
using WeatherAnalyzer.Weather.Api;

namespace WeatherAnalyzer;

public class WeatherAnalyzerProgram
(
    IGeocodeApi geocodeApi,
    IWeatherForecastApi weatherForecastApi,
    Func<IAsyncEnumerable<City>, Task<City?>> asyncCityChooser,
    IConsole console
)
{
    public async Task DownloadWeatherForecastsAsync(string locationSearch, FileInfo outputFile)
    {
        var storedForecast = await LoadWeatherForecastsAsync(locationSearch);

        await using var outputStream = outputFile.OpenWrite();
        await JsonSerializer.SerializeAsync(outputStream, storedForecast);
    }

    public async Task AnalyzeWeatherForecastAsync(string locationSearch)
    {
        var storedForecasts = await LoadWeatherForecastsAsync(locationSearch);
        var weatherForecasts = await storedForecasts.WeatherForecasts.ToListAsync();

        AnalyzeWeatherForecasts(weatherForecasts);
        console.Out.WriteLine();

        var byDay = from forecast in weatherForecasts
            group forecast by DateOnly.FromDateTime(forecast.Time);

        foreach (var day in byDay)
        {
            AnalyzeWeatherForecasts(day.ToList());
            console.Out.WriteLine();
        }
    }

    private async Task<StoredWeatherForecast> LoadWeatherForecastsAsync(string locationSearch)
    {
        var cities = geocodeApi.FindCitiesAsync(locationSearch);

        var city = await asyncCityChooser(cities) ?? throw new Exception("No city was found");

        var forecasts = weatherForecastApi.GetWeatherForecastsAsync(city);

        return new StoredWeatherForecast(city, forecasts);
    }

    private void AnalyzeWeatherForecasts(IList<WeatherForecast> forecasts)
    {
        var days = forecasts.Select(f => DateOnly.FromDateTime(f.Time)).ToList();

        var dayMin = days.Min();
        var dayMax = days.Max();
        console.WriteLine(dayMin == dayMax ? $"{dayMin}:" : $"{dayMin}..{dayMax}:");

        const string tab = "\t";

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Temperature", f => f.Temperature));

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Apparent temperature", f => f.ApparentTemperature));

        var precipitationAnalysis =
            CreateAnalysis(forecasts, "Precipitation amount", f => f.Precipitation.Amount);
        var precipitationProbabilityMin = forecasts.Min(f => f.Precipitation.Probability);
        var precipitationProbabilityMax = forecasts.Max(f => f.Precipitation.Probability);
        precipitationAnalysis =
            $"{precipitationAnalysis}, probability: {precipitationProbabilityMin:P1}..{precipitationProbabilityMax:P1}";
        console.Write(tab);
        console.WriteLine(precipitationAnalysis);

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Humidity", f => f.Humidity));

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Dew point", f => f.DewPoint));

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Cloud cover", f => f.CloudCover));

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Visibility", f => f.Visibility));

        console.Write(tab);
        console.WriteLine(CreateAnalysis(forecasts, "Wind", f => f.Wind.Speed));
    }

    private static string CreateAnalysis(
        IList<WeatherForecast> forecasts,
        string by,
        Func<WeatherForecast, ValueUnit<float>> selector
    )
    {
        var min = forecasts.MinBy(ValueSelector);
        var max = forecasts.MaxBy(ValueSelector);
        var avg = forecasts.Average(ValueSelector);

        if (min is null || max is null)
        {
            throw new ArgumentException("List was empty", nameof(forecasts));
        }

        var minValue = selector(min).Value;
        var maxValue = selector(max).Value;
        var unit = selector(min).Unit;

        if (unit == "%")
        {
            // 100% == new ValueUnit(100, "%")
            // Divide everything, dotnet multiplies it back
            minValue /= 100;
            maxValue /= 100;
            avg /= 100;
            return $"{by}: {minValue:P1}..{maxValue:P1}, average: {avg:P1}";
        }

        return $"{by}: {minValue:F1}{unit}..{maxValue:F1}{unit}, average: {avg:F1}{unit}";

        float ValueSelector(WeatherForecast forecast) => selector(forecast).Value;
    }
}
