namespace WeatherAnalyzer.Util;

public record struct ValueUnit<T>(T Value, string Unit) where T : struct;
