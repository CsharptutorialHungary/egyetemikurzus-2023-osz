using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Weather;

namespace WeatherAnalyzer.Util;

public record StoredWeatherForecast(City City, IAsyncEnumerable<WeatherForecast> WeatherForecasts);
