namespace WeatherAnalyzer.Geocode;

public record struct Location(float Lat, float Lon)
{
    public override string ToString() => $"{Lat}:{Lon}";
}
