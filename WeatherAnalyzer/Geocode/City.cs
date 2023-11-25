namespace WeatherAnalyzer.Geocode;

public record City(string Name, string CountryCode, string Area, Location Location, long Population)
{
    public override string ToString() => $"{Name} in {Area}, {CountryCode}, at {Location}, population: {Population}";
}
