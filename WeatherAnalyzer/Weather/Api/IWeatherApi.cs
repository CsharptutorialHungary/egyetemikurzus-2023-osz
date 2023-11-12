using WeatherAnalyzer.Geocode;

namespace WeatherAnalyzer.Weather.Api;

public interface IWeatherApi
{
    IEnumerable<WeatherForecast> GetWeatherForecasts(Location location);
}
