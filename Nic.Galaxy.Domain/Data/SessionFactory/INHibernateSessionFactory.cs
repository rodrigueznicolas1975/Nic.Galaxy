using NHibernate;

namespace Nic.Galaxy.Domain.Data.SessionFactory
{
    /// <summary>
    /// Interface IFluentNHibernateSessionBuilder
    /// </summary>
    public interface INHibernateSessionFactory
    {
        /// <summary>
        /// Gets the session factory.
        /// </summary>
        /// <returns>ISessionFactory.</returns>
        ISessionFactory GetSessionFactory();
    }
}
