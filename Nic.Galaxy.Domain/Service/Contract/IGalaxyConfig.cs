using System.Collections.Generic;
using Nic.Galaxy.Domain.Entity.Galaxy;

namespace Nic.Galaxy.Domain.Service.Contract
{
    public interface IGalaxyConfig
    {
        string GalaxyName { get; set; }
        bool IsDefault { get; set; }
        int Years { get; set; }
        IList<Planet> Planets { get; set; }
        bool IsValid();
        WeatherForecast CreatWeatherForecast(Entity.Galaxy.Galaxy galaxy, int day);
    }
}