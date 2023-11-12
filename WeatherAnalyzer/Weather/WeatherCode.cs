namespace WeatherAnalyzer.Weather;

// https://open-meteo.com/en/docs#weathervariables
public enum WeatherCode
{
    ClearSky = 0,
    MainlyClear = 1,
    PartlyClear = 2,
    Overcast = 3,
    Fog = 45,
    DepositingRimeFog = 48,
    LightDrizzle = 51,
    ModerateDrizzle = 53,
    DenseDrizzle = 55,
    LightFreezingDrizzle = 56,
    DenseFreezingDrizzle = 57,
    SlightRain = 61,
    ModerateRain = 63,
    HeavyRain = 65,
    LightFreezingRain = 66,
    HeavyFreezingRain = 67,
    SlightSnowfall = 71,
    ModerateSnowfall = 73,
    HeavySnowfall = 75,
    SnowGrains = 77,
    SlightRainShowers = 80,
    ModerateRainShowers = 81,
    ViolentRainShowers = 82,
    SlightSnowShowers = 85,
    HeavySnowShowers = 86,
    Thunderstorm = 95,
    SlightHail = 96,
    HeavyHail = 99
}
