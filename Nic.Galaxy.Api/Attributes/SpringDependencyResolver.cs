using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Spring.Context;
using Spring.Context.Support;

namespace Nic.Galaxy.Api.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
    public class SpringDependencyResolver : IDependencyResolver
    {
        private static IApplicationContext _applicationContext;

        private static IApplicationContext SpringContext => _applicationContext ?? (_applicationContext = ContextRegistry.GetContext());

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>
        /// The retrieved service.
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return GetSpringContextObject(serviceType);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>
        /// The retrieved collection of services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return new List<object> {GetSpringContextObject(serviceType)};
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static object GetSpringContextObject(Type serviceType)
        {
            var dictionary = SpringContext.GetObjectsOfType(serviceType).GetEnumerator();
            dictionary.MoveNext();
            return dictionary.Current.Value;
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Method intentionally left empty.
        }
    }
}