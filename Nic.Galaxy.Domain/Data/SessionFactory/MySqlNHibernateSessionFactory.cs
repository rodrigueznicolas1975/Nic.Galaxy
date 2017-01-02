using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Nic.Galaxy.Domain.Data.Mapping;

namespace Nic.Galaxy.Domain.Data.SessionFactory
{
    /// <summary>
    /// Class MySqlNHibernateSessionBuilder.
    /// </summary>
    public class MySqlNHibernateSessionFactory : INHibernateSessionFactory
    {
        /// <summary>
        /// The session factory
        /// </summary>
        private ISessionFactory _sessionFactory;

        protected string ConnectionString { get; set; }

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        /// <returns>ISessionFactory.</returns>
        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                var scope = ConfigurationManager.AppSettings["Database.Scope"];
                _sessionFactory = Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString))
                    .Mappings(mappings => mappings.FluentMappings.AddFromAssemblyOf<GalaxyMapping>())
                    .ExposeConfiguration(config => config.SetProperty(Environment.CurrentSessionContextClass, scope))
                    .ExposeConfiguration(config => config.SetProperty(Environment.Dialect, "NHibernate.Dialect.MySQL55Dialect"))
                    .ExposeConfiguration(config => config.SetProperty(Environment.ConnectionString, ConnectionString))
                    .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
                    .BuildConfiguration()
                    .BuildSessionFactory();
            }
            return _sessionFactory;
        }
    }
}