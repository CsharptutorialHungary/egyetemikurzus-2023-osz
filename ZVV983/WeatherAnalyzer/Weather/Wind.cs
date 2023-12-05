using WeatherAnalyzer.Util;

namespace WeatherAnalyzer.Weather;

public record struct Wind(ValueUnit<float> Speed, ValueUnit<int> Direction);
