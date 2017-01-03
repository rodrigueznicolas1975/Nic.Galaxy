using System.Collections.Generic;
using Nic.Galaxy.Domain.Entity.Galaxy;

namespace Nic.Galaxy.Domain.Data.Repository.Contract
{
    public interface IGalaxyRepository : IBaseRepository<Entity.Galaxy.Galaxy, int>
    {
        Entity.Galaxy.Galaxy GetByName(string name);
        void UpdatePlanets(Entity.Galaxy.Galaxy galaxy, IList<Planet> planets);
    }
}
