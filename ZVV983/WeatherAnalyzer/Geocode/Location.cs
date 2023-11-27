namespace WeatherAnalyzer.Geocode;

public readonly record struct Location(float Lat, float Lon)
{
    public override string ToString() => $"{Lat}:{Lon}";
}
