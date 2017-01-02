using Nic.Galaxy.Domain.ViewModel;

namespace Nic.Galaxy.Domain.Service.Contract
{
    public interface IGalaxyService
    {
        ResponseWeatherStatistic WeatherStatistic(string planetName, string weatherName);

        ResponseWeatherStatistic WeatherStatistic(string galaxyName, string planetName, string weatherName);

        ResponseWeather GetWeatherByDay(int day);

        ResponseWeather GetWeatherByDay(string galaxyName, int day);

        void Initialize(bool force);

        void Initialize(string galaxyName, bool force);
    }
}
