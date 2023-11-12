using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Weather;

public record struct Precipitation(float Probability, ValueUnit<float> Amount);
