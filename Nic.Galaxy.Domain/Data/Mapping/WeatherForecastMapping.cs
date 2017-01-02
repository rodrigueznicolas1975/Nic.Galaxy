using FluentNHibernate.Mapping;
using Nic.Galaxy.Domain.Entity.Galaxy;
using Nic.Galaxy.Domain.Enum;

namespace Nic.Galaxy.Domain.Data.Mapping
{
    internal class WeatherForecastMapping : ClassMap<WeatherForecast>
    {
        public WeatherForecastMapping()
        {
            Table("WeatherForecast");

            Id(x => x.Id);

            References(x => x.Galaxy, "GalaxyId").Not.Nullable().Cascade.All().Not.LazyLoad();
            Map(x => x.Day).Not.Nullable();
            Map(x => x.Weather, "WeatherId").CustomType<Weather>().Not.Nullable();
            Map(x => x.Perimeter).Not.Nullable();
        }
    }
}
