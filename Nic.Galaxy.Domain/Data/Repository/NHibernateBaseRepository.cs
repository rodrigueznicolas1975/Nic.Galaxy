using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using Nic.Galaxy.Domain.Data.Repository.Contract;
using Nic.Galaxy.Domain.Data.SessionFactory;

namespace Nic.Galaxy.Domain.Data.Repository
{
    public abstract class NHibernateBaseRepository<T, TId> : IBaseRepository<T, TId>
    {
        public INHibernateSessionFactory SessionFactory { get; set; }

        public ISession Session
        {
            get { return SessionFactory.GetSessionFactory().GetCurrentSession(); }
        }

        public T GetById(TId id)
        {
            return Session.Get<T>(id);
        }

        public void Remove(T entity)
        {
            Session.Delete(entity);
        }

        public TId Save(T entity)
        {
            return (TId)Session.Save(entity);
        }

        public void Attach(T entity)
        {
            Session.Update(entity);
        }

        public void Detach(T entity)
        {
            Session.Evict(entity);
        }

        public IQueryable<T> Query()
        {
            return Session.Query<T>();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> query)
        {
            return Session.Query<T>().Where(query);
        }
    }
}
