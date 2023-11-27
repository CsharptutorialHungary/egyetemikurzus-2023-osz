using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using WeatherAnalyzer.Cli;

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
        if (response?.Cities is null) yield break;

        foreach (var city in response.Cities)
        {
            yield return new City(
                city.Name,
                city.CountryCode,
                city.Admin1,
                new Location(city.Lat, city.Lon),
                city.Population
            );
        }
    }

    private record ApiResponse([property: JsonPropertyName("results")] IEnumerable<ApiResponseCity>? Cities);

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")] // Instantiated by JSON deserializer
    private record ApiResponseCity
    (
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("country_code")] string CountryCode,
        [property: JsonPropertyName("admin1")] string Admin1,
        [property: JsonPropertyName("latitude")] float Lat,
        [property: JsonPropertyName("longitude")] float Lon,
        [property: JsonPropertyName("population")] int Population
    );
}
