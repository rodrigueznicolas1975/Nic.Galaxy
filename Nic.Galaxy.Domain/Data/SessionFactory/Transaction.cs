using System;
using NHibernate;
using NHibernate.Context;
using Spring.Context.Support;

namespace Nic.Galaxy.Domain.Data.SessionFactory
{
    public static class Transaction
    {
        public static void ExecuteInSession(Action action)
        {
            var sessionFactory = (INHibernateSessionFactory)ContextRegistry.GetContext().GetObject("INHibernateSessionFactory");
            ISession session;
            try
            {
                session = sessionFactory.GetSessionFactory().OpenSession();
                CurrentSessionContext.Bind(session);
                action.Invoke();
            }
            finally
            {
                session = CurrentSessionContext.Unbind(sessionFactory.GetSessionFactory());
                session.Clear();
                session.Close();
            }
        }

        public static void ExecuteInTransaction(Action action)
        {
            var sessionFactory = (INHibernateSessionFactory)ContextRegistry.GetContext().GetObject("INHibernateSessionFactory");
            ISession session;
            try
            {
                session = sessionFactory.GetSessionFactory().OpenSession();
                CurrentSessionContext.Bind(session);
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        action.Invoke();
                        if (transaction.IsActive)
                        {
                            session.Flush();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (!transaction.WasCommitted && !transaction.WasRolledBack)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            finally
            {
                session = CurrentSessionContext.Unbind(sessionFactory.GetSessionFactory());
                session.Clear();
                session.Close();
                session.Dispose();
            }
        }
    }
}
