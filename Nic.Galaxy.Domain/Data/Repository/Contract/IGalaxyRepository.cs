namespace Nic.Galaxy.Domain.Data.Repository.Contract
{
    public interface IGalaxyRepository : IBaseRepository<Entity.Galaxy.Galaxy, int>
    {
        Entity.Galaxy.Galaxy GetByName(string name);
    }
}
