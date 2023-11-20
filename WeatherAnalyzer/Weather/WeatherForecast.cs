using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Weather;

public record WeatherForecast(
    ValueUnit<DateTime> Time,
    Location Location,
    ValueUnit<float> Temperature,
    ValueUnit<float> ApparentTemperature,
    Precipitation Precipitation,
    float Humidity,
    ValueUnit<float> DewPoint,
    ValueUnit<WeatherCode> WeatherCode,
    Wind Wind
);
