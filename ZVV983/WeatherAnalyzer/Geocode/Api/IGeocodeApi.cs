namespace WeatherAnalyzer.Geocode.Api;

public interface IGeocodeApi
{
    IAsyncEnumerable<City> FindCitiesAsync(string name);
}
