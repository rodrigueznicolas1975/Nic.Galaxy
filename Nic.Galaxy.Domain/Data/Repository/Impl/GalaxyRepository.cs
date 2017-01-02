using System.Linq;
using Nic.Galaxy.Domain.Data.Repository.Contract;

namespace Nic.Galaxy.Domain.Data.Repository.Impl
{
    public class GalaxyRepository : NHibernateBaseRepository<Entity.Galaxy.Galaxy, int>, IGalaxyRepository
    {
        public Entity.Galaxy.Galaxy GetByName(string name)
        {
            return Query(x => x.Name.Equals(name)).FirstOrDefault();
        }
    }
}
