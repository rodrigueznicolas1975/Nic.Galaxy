using System.Collections.Generic;
using System.Linq;
using Nic.Galaxy.Domain.Data.Repository.Contract;
using Nic.Galaxy.Domain.Entity.Galaxy;
using Nic.Galaxy.Domain.Enum;

namespace Nic.Galaxy.Domain.Data.Repository.Impl
{
    public class WeatherForecastRepository : NHibernateBaseRepository<WeatherForecast, int>, IWeatherForecastRepository
    {
        public int CountByGalaxy(int galaxyId)
        {
            return Session.QueryOver<WeatherForecast>()
                 .Where(x => x.Galaxy.Id == galaxyId)
                 .RowCount();
        }

        public int CounWeatherDaysByGalaxy(Weather weather, int galaxyId, int day)
        {
            return Session.QueryOver<WeatherForecast>()
                .Where(x => x.Galaxy.Id == galaxyId && x.Weather == weather && x.Day <= day)
                .RowCount();
        }

        public IList<int> DayMaxPerimeterByGalaxy(int galaxyId, int day)
        {
            var weatherForecasts = Query(x => x.Galaxy.Id == galaxyId && x.Day <= day && x.Perimeter > 0).ToList();
            var weatherForecast = weatherForecasts.Max();
            var weatherForeCastsMaxPerimeters = weatherForecasts.Where(x => x.Perimeter.Equals(weatherForecast.Perimeter)).Select(x => x.Day).ToList();
            return weatherForeCastsMaxPerimeters;
        }

        public void RemoveByGalaxy(int galaxyId)
        {
            Session.CreateSQLQuery("DELETE FROM WeatherForecast WHERE GalaxyId=:galaxyId")
                .SetParameter("galaxyId", galaxyId)
                .ExecuteUpdate();
            Session.Flush();
        }

        public WeatherForecast GetByDay(int galaxyId, int day)
        {
            return Query(x => x.Galaxy.Id == galaxyId && x.Day == day).FirstOrDefault();
        }
    }
}