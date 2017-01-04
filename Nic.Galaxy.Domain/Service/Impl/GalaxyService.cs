using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nic.Galaxy.Domain.Data.Repository.Contract;
using Nic.Galaxy.Domain.Enum;
using Nic.Galaxy.Domain.Exception;
using Nic.Galaxy.Domain.Service.Contract;
using Nic.Galaxy.Domain.ViewModel;

namespace Nic.Galaxy.Domain.Service.Impl
{
    public class GalaxyService : IGalaxyService
    {
        protected IGalaxyRepository GalaxyRepository { get; set; }
        protected IWeatherForecastRepository WeatherForecastRepository { get; set; }
        protected IList<IGalaxyConfig> GalaxyConfigs { get; set; }

        private IGalaxyConfig _galaxyConfig;
        private Entity.Galaxy.Galaxy _galaxy;

        public ResponseWeatherStatistic WeatherStatistic(string planetName, string weatherName)
        {
            return WeatherStatistic(string.Empty, planetName, weatherName);
        }

        public ResponseWeatherStatistic WeatherStatistic(string galaxyName, string planetName, string weatherName)
        {
            Weather weather;
            if (!System.Enum.TryParse(weatherName, true, out weather) || !System.Enum.IsDefined(typeof(Weather), weather))
            {
                throw new ValidException(string.Format("El clima '{0}' es inexistente", weatherName));
            }

            GetGalaxyConfig(galaxyName, false, true);
            var planet = _galaxy.Planets.First(x => x.Name.ToLowerInvariant().Equals(planetName.ToLowerInvariant()));
            var maxDay = TotalDays(planet.Velocity, _galaxyConfig.Years);
            return new ResponseWeatherStatistic
            {
                Clima = weather.ToString(),
                DiaMaximoLluvia = weather == Weather.Lluvia ? WeatherForecastRepository.DayMaxPerimeterByGalaxy(_galaxy.Id, maxDay) : -1,
                TotalPeriodo = WeatherForecastRepository.CounWeatherDaysByGalaxy(weather, _galaxy.Id, maxDay)
            };
        }

        public ResponseWeather GetWeatherByDay(int day)
        {
            return GetWeatherByDay(string.Empty, day);
        }

        public ResponseWeather GetWeatherByDay(string galaxyName, int day)
        {
            GetGalaxyConfig(galaxyName, false, true);
            var totalDays = MaxTotalDayInGalaxy();

            if (day < 0 || day > totalDays)
            {
                throw new ValidException(string.Format("El día debe estar comprendido entre 0 y {0}", totalDays));
            }
            
            var weatherForecast = WeatherForecastRepository.GetByDay(_galaxy.Id, day);
            return new ResponseWeather
            {
                Clima = weatherForecast.Weather.ToString(),
                Dia = weatherForecast.Day
            };
        }

        public void Initialize(bool force)
        {
            Initialize(string.Empty, force);
        }

        public async void Initialize(string galaxyName, bool force)
        {
            GetGalaxyConfig(galaxyName, true, false);
            var totalDays = MaxTotalDayInGalaxy();

            if (force || WeatherForecastRepository.CountByGalaxy(_galaxy.Id) != totalDays +1)
            {
                WeatherForecastRepository.RemoveByGalaxy(_galaxy.Id);
                var dayEnum = 0;
                IList<int> days = (new int[totalDays+1]).Select(x=> dayEnum++).ToList();

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                var tasks = days.Select(async day =>
                {
                    var weatherForecast = _galaxyConfig.CreatWeatherForecast(_galaxy, day);
                    WeatherForecastRepository.Save(weatherForecast);
                });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

                await Task.WhenAll(tasks);
            }
        }

        private void GetGalaxyConfig(string galaxyName, bool forceCreateGalaxy, bool validGalaxy)
        {
            if (!string.IsNullOrWhiteSpace(galaxyName))
            {
                _galaxyConfig = GalaxyConfigs.FirstOrDefault(x => x.GalaxyName.ToLowerInvariant().Equals(galaxyName.ToLowerInvariant()));
                if (_galaxyConfig == null)
                {
                    throw new ValidException(string.Format("La galaxia '{0}' es inexistente", galaxyName));
                }
            }
            else
            {
                _galaxyConfig = GalaxyConfigs.FirstOrDefault(x => x.IsDefault);
                if (_galaxyConfig == null)
                {
                    throw new ValidException("No se encontró la galaxia");
                }
            }
            if (!_galaxyConfig.IsValid())
            {
                throw new ValidException(string.Format("La configuración del sistema de la galaxia {0} no es válida, contáctese con el administrador.", _galaxyConfig.GalaxyName));
            }
            GetGalaxy(forceCreateGalaxy);
            if (validGalaxy)
            {
                ValidGalaxy();
            }
        }

        private void GetGalaxy(bool forceCreateGalaxy)
        {
            _galaxy = GalaxyRepository.GetByName(_galaxyConfig.GalaxyName);
            if (forceCreateGalaxy)
            {
                if (_galaxy == null)
                {
                    _galaxy = new Entity.Galaxy.Galaxy
                    {
                        Name = _galaxyConfig.GalaxyName,
                        Planets = _galaxyConfig.Planets
                    };
                    GalaxyRepository.Save(_galaxy);
                }
                else
                {
                    _galaxy.Name = _galaxyConfig.GalaxyName;
                    GalaxyRepository.UpdatePlanets(_galaxy, _galaxyConfig.Planets);
                }
            }
        }

        private void ValidGalaxy()
        {
            if (_galaxy != null)
            {
                var totalDays = MaxTotalDayInGalaxy() + 1;
                if (WeatherForecastRepository.CountByGalaxy(_galaxy.Id) == totalDays)
                {
                    return;
                }
            }
            throw new ValidException("No se encuentra inicializado los datos necesarios para poder procesarlos. Debe primero inicializarlo");
        }

        private int MaxTotalDayInGalaxy()
        {
            var minPlanetVelocity = _galaxy.Planets.Min(x => x.Velocity);
            return TotalDays(minPlanetVelocity, _galaxyConfig.Years);
        }

        private static int TotalDays(int planetVelocity, int years)
        {
            return (years*(360/planetVelocity)) - 1;
        }
    }
}
