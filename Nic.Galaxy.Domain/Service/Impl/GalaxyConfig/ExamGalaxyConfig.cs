using System.Collections.Generic;
using Nic.Galaxy.Domain.Entity.Galaxy;
using Nic.Galaxy.Domain.Enum;
using Nic.Galaxy.Domain.Service.Contract;
using Nic.Galaxy.Utilities.Model;

namespace Nic.Galaxy.Domain.Service.Impl.GalaxyConfig
{
    internal class ExamGalaxyConfig : IGalaxyConfig
    {
        public string GalaxyName { get; set; }
        public bool IsDefault { get; set; }
        public int Years { get; set; }
        public IList<Planet> Planets { get; set; }

        public bool IsValid()
        {
            return Planets.Count == 3;
        }

        public WeatherForecast CreatWeatherForecast(Entity.Galaxy.Galaxy galaxy, int day)
        {
            var sunCoordinate = new Coordinate {X = 0, Y = 0};
            var planetACoordinate = CreateCoordinate(Planets[0], day);
            var planetBCoordinate = CreateCoordinate(Planets[1], day);
            var planetCCoordinate = CreateCoordinate(Planets[2], day);

            var weather = Weather.Normal;
            double perimeter = 0;

            if (Utilities.Calculation.Trigonometry.AreThreePointsAligned(planetACoordinate, planetBCoordinate, planetCCoordinate))
            {
                if (Utilities.Calculation.Trigonometry.AreThreePointsAligned(sunCoordinate, planetACoordinate, planetBCoordinate))
                {
                    weather = Weather.Sequia;
                }
                else
                {
                    weather = Weather.Optimo;
                }
            }
            else if(Utilities.Calculation.Trigonometry.IsPointIntoTriangle(sunCoordinate, planetACoordinate, planetBCoordinate, planetCCoordinate))
            {
                weather = Weather.Lluvia;
                perimeter = Utilities.Calculation.Trigonometry.CalculatePerimeterTriangle(planetACoordinate, planetBCoordinate, planetCCoordinate);
            }

            return new WeatherForecast
            {
                Day = day,
                Galaxy = galaxy,
                Weather = weather,
                Perimeter = perimeter
            };
        }

        private static Coordinate CreateCoordinate(Planet planet, int day)
        {
            var xCoordinate = Utilities.Calculation.Trigonometry.CalculateXCoordinate(planet.Radius, planet.Velocity, planet.Direction, day);
            var yCoordinate = Utilities.Calculation.Trigonometry.CalculateYCoordinate(planet.Radius, planet.Velocity, planet.Direction, day);
            return new Coordinate
            {
                X = xCoordinate,
                Y = yCoordinate
            };
        }
    }
}
