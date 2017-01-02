using FluentNHibernate.Mapping;

namespace Nic.Galaxy.Domain.Data.Mapping
{
    internal class GalaxyMapping : ClassMap<Entity.Galaxy.Galaxy>
    {
        public GalaxyMapping()
        {
            Table("galaxy");

            Id(x => x.Id);

            Map(x => x.Name).Not.Nullable().Unique();

            HasMany(x=> x.Planets).KeyColumn("GalaxyId").Cascade.AllDeleteOrphan().Inverse().Not.LazyLoad();
        } 
    }
}
