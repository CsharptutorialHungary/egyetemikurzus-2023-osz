using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using WeatherAnalyzer.Cli;
using static WeatherAnalyzer.Util.Util;

namespace WeatherAnalyzer.Geocode.Api.OpenMeteo;

public class OpenMeteoGeocodeApi(string apiUrl, string languageCode) : IGeocodeApi, ICliDiscoverableApi
{
    private const string DefaultLanguage = "en";
    private const string DefaultApiUrl = "https://geocoding-api.open-meteo.com/v1/search";
    private const int MaxResultCount = 10;

    [SuppressMessage("ReSharper", "UnusedMember.Global")] // Used by reflection
    public OpenMeteoGeocodeApi(IConfiguration configuration) : this(
        configuration["OpenMeteo:GeocodeApiUrl"] ?? DefaultApiUrl,
        configuration["Language"] ?? DefaultLanguage
    )
    {
    }

    public string CliName => "open-meteo";

    public async IAsyncEnumerable<City> FindCitiesAsync(string name)
    {
        using var http = new HttpClient();

        var stream = await http.GetStreamAsync(
            $"{apiUrl}?name={name}&count={MaxResultCount}&language={languageCode}&format=json");

        var response = await JsonSerializer.DeserializeAsync<ApiResponse>(stream);
        if (response is null) yield break;

        foreach (var city in response.Cities)
        {
            yield return new City(
                ApiResultNullCheck(city.Name),
                ApiResultNullCheck(city.CountryCode),
                ApiResultNullCheck(city.Admin1),
                new Location(city.Lat, city.Lon),
                city.Population
            );
        }
    }

    private class ApiResponse
    {
        [JsonPropertyName("results")]
        public IEnumerable<ApiResponseCity> Cities { get; set; } = Enumerable.Empty<ApiResponseCity>();
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")] // Instantiated by JSON deserializer
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")] // Used by JSON deserializer
    private class ApiResponseCity
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("country_code")] public string? CountryCode { get; set; }
        [JsonPropertyName("admin1")] public string? Admin1 { get; set; }
        [JsonPropertyName("latitude")] public float Lat { get; set; }
        [JsonPropertyName("longitude")] public float Lon { get; set; }
        [JsonPropertyName("population")] public int Population { get; set; }
    }
}
