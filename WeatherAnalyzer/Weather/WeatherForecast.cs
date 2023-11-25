using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Weather;

public record WeatherForecast(
    DateTime Time,
    Location Location,
    ValueUnit<float> Temperature,
    ValueUnit<float> ApparentTemperature,
    Precipitation Precipitation,
    ValueUnit<float> Humidity,
    ValueUnit<float> DewPoint,
    WeatherCode WeatherCode,
    Wind Wind
);
