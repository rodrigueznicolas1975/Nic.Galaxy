using FluentNHibernate.Mapping;
using Nic.Galaxy.Domain.Entity.Galaxy;

namespace Nic.Galaxy.Domain.Data.Mapping
{
    internal class PlanetMapping : ClassMap<Planet>
    {
        public PlanetMapping()
        {
            Table("planet");

            Id(x => x.Id);

            Map(x => x.Name).Not.Nullable();
            Map(x => x.Direction).Not.Nullable();
            Map(x => x.Radius).Not.Nullable();
            Map(x => x.Velocity).Not.Nullable();

            References(x => x.Galaxy, "GalaxyId");
        }
    }
}
