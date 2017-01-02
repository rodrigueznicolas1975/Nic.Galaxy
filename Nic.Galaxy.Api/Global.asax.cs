using System.Web;
using System.Web.Http;
using Nic.Galaxy.Api.Attributes;
using Nic.Galaxy.Api.Serialization;

namespace Nic.Galaxy.Api
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Serialization
            GlobalConfiguration.Configure(ConfigureWebApiSerialization);

            //Custom Exceptions
            GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionAttribute());

            //Spring
            GlobalConfiguration.Configuration.DependencyResolver = new SpringDependencyResolver();
        }

        /// <summary>
        /// Configure web API serialization.
        /// </summary>
        /// <param name="config">The configuration.</param>
        protected void ConfigureWebApiSerialization(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.JsonFormatter);

            config.Formatters.Add(ApiJsonMediaTypeFormatter.Default);
        }
    }
}
