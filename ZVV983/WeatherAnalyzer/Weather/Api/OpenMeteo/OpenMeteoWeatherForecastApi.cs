using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Weather.Api.OpenMeteo;

public class OpenMeteoWeatherForecastApi(string apiUrl) : IWeatherForecastApi, ICliDiscoverableApi
{
    private const string DefaultApiUrl = "https://api.open-meteo.com/v1/forecast";

    [SuppressMessage("ReSharper", "UnusedMember.Global")] // Used by reflection
    public OpenMeteoWeatherForecastApi(IConfiguration configuration) : this(
        configuration["OpenMeteo:ForecastApiUrl"] ?? DefaultApiUrl
    )
    {
    }

    public string CliName => "open-meteo";

    public async IAsyncEnumerable<WeatherForecast> GetWeatherForecastsAsync(City city)
    {
        using var http = new HttpClient();

        var (lat, lon) = city.Location;
        var stream = await http.GetStreamAsync(
            $"{apiUrl}?latitude={lat}&longitude={lon}&hourly=temperature_2m,relative_humidity_2m,dew_point_2m,apparent_temperature,precipitation_probability,precipitation,weather_code,cloud_cover,visibility,wind_speed_10m,wind_direction_10m");

        var response = await JsonSerializer.DeserializeAsync<ApiResponse>(stream);
        if (response is null) yield break;

        var units = response.HourlyUnits;
        var values = response.HourlyValues;

        for (int i = 0, count = response.HourlyValues.Count; i < count; i++)
        {
            yield return new WeatherForecast(
                values.Time[i],
                new Location(response.Lat, response.Lon),
                new ValueUnit<float>(values.Temperature[i], units.Temperature),
                new ValueUnit<float>(values.ApparentTemperature[i], units.ApparentTemperature),
                new Precipitation(
                    values.PrecipitationProbability[i] / 100f,
                    new ValueUnit<float>(values.PrecipitationAmount[i], units.PrecipitationAmount)
                ),
                new ValueUnit<float>(values.RelativeHumidity[i], units.RelativeHumidity),
                new ValueUnit<float>(values.DewPoint[i], units.DewPoint),
                values.WeatherCode[i],
                new ValueUnit<float>(values.CloudCover[i], units.CloudCover),
                new ValueUnit<float>(values.Visibility[i], units.Visibility),
                new Wind(
                    new ValueUnit<float>(values.WindSpeed[i], units.WindSpeed),
                    new ValueUnit<int>(values.WindDirection[i], units.WindDirection)
                )
            );
        }
    }

    private record ApiResponse
    (
        [property: JsonPropertyName("latitude")] float Lat,
        [property: JsonPropertyName("longitude")] float Lon,
        [property: JsonPropertyName("hourly_units")] ApiResponseUnits HourlyUnits,
        [property: JsonPropertyName("hourly")] ApiResponseHourlyValues HourlyValues
    );

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")] // Instantiated by JSON deserializer
    private record ApiResponseUnits
    (
        [property: JsonPropertyName("time")] string Time,
        [property: JsonPropertyName("temperature_2m")] string Temperature,
        [property: JsonPropertyName("relative_humidity_2m")] string RelativeHumidity,
        [property: JsonPropertyName("dew_point_2m")] string DewPoint,
        [property: JsonPropertyName("apparent_temperature")] string ApparentTemperature,
        [property: JsonPropertyName("precipitation_probability")] string PrecipitationProbability,
        [property: JsonPropertyName("precipitation")] string PrecipitationAmount,
        [property: JsonPropertyName("weather_code")] string WeatherCode,
        [property: JsonPropertyName("cloud_cover")] string CloudCover,
        [property: JsonPropertyName("visibility")] string Visibility,
        [property: JsonPropertyName("wind_speed_10m")] string WindSpeed,
        [property: JsonPropertyName("wind_direction_10m")] string WindDirection
    );

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")] // Instantiated by JSON deserializer
    private record ApiResponseHourlyValues
    (
        [property: JsonPropertyName("time")] IList<DateTime> Time,
        [property: JsonPropertyName("temperature_2m")] IList<float> Temperature,
        [property: JsonPropertyName("relative_humidity_2m")] IList<int> RelativeHumidity,
        [property: JsonPropertyName("dew_point_2m")] IList<float> DewPoint,
        [property: JsonPropertyName("apparent_temperature")] IList<float> ApparentTemperature,
        [property: JsonPropertyName("precipitation_probability")] IList<int> PrecipitationProbability,
        [property: JsonPropertyName("precipitation")] IList<float> PrecipitationAmount,
        [property: JsonPropertyName("weather_code")] IList<WeatherCode> WeatherCode,
        [property: JsonPropertyName("cloud_cover")] IList<int> CloudCover,
        // Firefox scammed me. Visibility not an int, because it has a .00 suffix making it a float
        [property: JsonPropertyName("visibility")] IList<float> Visibility,
        [property: JsonPropertyName("wind_speed_10m")] IList<float> WindSpeed,
        [property: JsonPropertyName("wind_direction_10m")] IList<int> WindDirection
    )
    {
        public int Count => Time.Count;
    }
}
