using WeatherAnalyzer.Geocode;
using WeatherAnalyzer.Geocode.Api.Gps;

namespace WeatherAnalyzer.Tests.Geocode.Api.Gps;

public class GpsGeocodeApiTests
{
    [TestCase("10:10", 10f, 10f)]
    [TestCase("10.499:10.1", 10.499f, 10.1f)]
    [TestCase("-10:-10", -10f, -10f)]
    [TestCase("-10.499:-10.1", -10.499f, -10.1f)]
    [TestCase("2.71:-3.141592654", 2.71f, -3.141592654f)]
    [TestCase("-8:56.96124543", -8f, 56.96124543f)]
    public async Task FindCitiesAsync_CorrectInput_ReturnsCityWithCorrectLocation(
        string coords,
        float expectedLat,
        float expectedLon
    )
    {
        var geocodeApi = new GpsGeocodeApi();

        var cities = geocodeApi.FindCitiesAsync(coords);
        var city = await cities.FirstAsync();

        Assert.Multiple(() =>
        {
            Assert.That(city,
                Has.Property(nameof(City.Location)).Property(nameof(Location.Lat)).EqualTo(expectedLat).Within(0.1f));
            Assert.That(city,
                Has.Property(nameof(City.Location)).Property(nameof(Location.Lon)).EqualTo(expectedLon).Within(0.1f));
        });
    }

    [TestCase("91:0")]
    [TestCase("-91:0")]
    [TestCase("0:-181")]
    [TestCase("0:181")]
    public void FindCitiesAsync_OutOfRange_ThrowsArgumentOutOfRangeException(string coords)
    {
        var geocodeApi = new GpsGeocodeApi();

        var cities = geocodeApi.FindCitiesAsync(coords);

        Assert.That(async () => await cities.FirstAsync(), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase("too:many:colons")]
    [TestCase("too few colons")]
    [TestCase("gorillakecske")]
    public void FindCitiesAsync_MalformedInput_ThrowsArgumentException(string coords)
    {
        var geocodeApi = new GpsGeocodeApi();

        var cities = geocodeApi.FindCitiesAsync(coords);

        Assert.That(async () => await cities.FirstAsync(), Throws.ArgumentException);
    }


    [TestCase("not:float")]
    [TestCase("42:whoops")]
    [TestCase("negative:7")]
    [TestCase("forty:two")]
    [TestCase("1:")]
    [TestCase(":-2")]
    [TestCase(":")]
    public void FindCitiesAsync_MalformedInput_ThrowsNumberFormatException(string coords)
    {
        var geocodeApi = new GpsGeocodeApi();

        var cities = geocodeApi.FindCitiesAsync(coords);

        Assert.That(async () => await cities.FirstAsync(), Throws.TypeOf<FormatException>());
    }
}
