using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nic.Galaxy.Domain.Data.Repository.Contract
{
    public interface IBaseRepository<T, TId>
    {
        T GetById(TId id);

        TId Save(T entity);

        void Attach(T entity);

        void Detach(T entity);

        void Remove(T key);

        IQueryable<T> Query();

        IQueryable<T> Query(Expression<Func<T, bool>> query);
    }
}
