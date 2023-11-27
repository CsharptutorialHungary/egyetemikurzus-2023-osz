using Microsoft.Extensions.Configuration;
using WeatherAnalyzer.Cli;
using WeatherAnalyzer.Geocode.Api;
using WeatherAnalyzer.Geocode.Api.File;
using WeatherAnalyzer.Geocode.Api.Gps;
using WeatherAnalyzer.Geocode.Api.OpenMeteo;
using WeatherAnalyzer.Weather.Api;
using WeatherAnalyzer.Weather.Api.File;
using WeatherAnalyzer.Weather.Api.OpenMeteo;

namespace WeatherAnalyzer.Tests.Cli;

public class CliApiDiscovererTests
{
    [Test]
    public void LoadImplementations_IGeocodeApi_DictionaryKeyMatchesCliName()
    {
        var config = new ConfigurationBuilder().Build();
        var discoverer = new CliApiDiscoverer<IGeocodeApi>(config);

        var impls = discoverer.LoadImplementations();

        Assert.Multiple(() =>
        {
            foreach (var (name, impl) in impls)
            {
                Assert.That(impl,
                    Is.InstanceOf<ICliDiscoverableApi>().And.Property(nameof(ICliDiscoverableApi.CliName))
                        .EqualTo(name));
            }
        });
    }

    [Test]
    public void LoadImplementations_IWeatherForecastApi_DictionaryKeyMatchesCliName()
    {
        var config = new ConfigurationBuilder().Build();
        var discoverer = new CliApiDiscoverer<IWeatherForecastApi>(config);

        var impls = discoverer.LoadImplementations();

        Assert.Multiple(() =>
        {
            foreach (var (name, impl) in impls)
            {
                Assert.That(impl,
                    Is.InstanceOf<ICliDiscoverableApi>().And.Property(nameof(ICliDiscoverableApi.CliName))
                        .EqualTo(name));
            }
        });
    }

    [Test]
    public void LoadImplementations_IGeocodeApi_LoadsAllImplementationsFromAssembly()
    {
        var config = new ConfigurationBuilder().Build();
        var discoverer = new CliApiDiscoverer<IGeocodeApi>(config);

        var impls = discoverer.LoadImplementations();

        Assert.Multiple(() =>
        {
            Assert.That(impls, Contains.Key("file"));
            Assert.That(impls, Contains.Key("gps"));
            Assert.That(impls, Contains.Key("open-meteo"));

            Assert.That(impls["file"], Is.TypeOf<FileGeocodeApi>());
            Assert.That(impls["gps"], Is.TypeOf<GpsGeocodeApi>());
            Assert.That(impls["open-meteo"], Is.TypeOf<OpenMeteoGeocodeApi>());

            Assert.That(impls, Has.Count.EqualTo(3));
        });
    }

    [Test]
    public void LoadImplementations_IWeatherForecastApi_LoadsAllImplementationsFromAssembly()
    {
        var config = new ConfigurationBuilder().Build();
        var discoverer = new CliApiDiscoverer<IWeatherForecastApi>(config);

        var impls = discoverer.LoadImplementations();

        Assert.Multiple(() =>
        {
            Assert.That(impls, Contains.Key("file"));
            Assert.That(impls, Contains.Key("open-meteo"));

            Assert.That(impls["file"], Is.TypeOf<FileWeatherForecastApi>());
            Assert.That(impls["open-meteo"], Is.TypeOf<OpenMeteoWeatherForecastApi>());

            Assert.That(impls, Has.Count.EqualTo(2));
        });
    }
}
