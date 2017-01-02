using System.Collections.Generic;
using System.Linq;
using Nic.Galaxy.Domain.Data.Repository.Contract;
using Nic.Galaxy.Domain.Entity.Galaxy;

namespace Nic.Galaxy.Domain.Data.Repository.Impl
{
    public class GalaxyRepository : NHibernateBaseRepository<Entity.Galaxy.Galaxy, int>, IGalaxyRepository
    {
        public Entity.Galaxy.Galaxy GetByName(string name)
        {
            return Query(x => x.Name.Equals(name)).FirstOrDefault();
        }

        public void Update(Entity.Galaxy.Galaxy galaxy, IList<Planet> planets)
        {
            galaxy.Planets.Clear();
            foreach (var planet in planets)
            {
                galaxy.Planets.Add(new Planet
                {
                    Name = planet.Name,
                    Direction = planet.Direction,
                    Radius = planet.Radius,
                    Velocity = planet.Velocity,
                    Galaxy = galaxy
                });
            }

            Session.Flush();
        }
    }
}
