using Nic.Galaxy.Domain.Entity.Galaxy;
using Nic.Galaxy.Domain.Enum;

namespace Nic.Galaxy.Domain.Data.Repository.Contract
{
    public interface IWeatherForecastRepository : IBaseRepository<WeatherForecast, int>
    {
        int CountByGalaxy(int galaxyId);
        int CounWeatherDaysByGalaxy(Weather weather, int galaxyId, int days);
        int DayMaxPerimeterByGalaxy(int galaxyId, int day);
        void RemoveByGalaxy(int galaxyId);
        WeatherForecast GetByDay(int galaxyId, int day);
    }
}
