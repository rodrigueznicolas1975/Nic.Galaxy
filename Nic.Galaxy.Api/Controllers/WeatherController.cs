using System.Web.Http;
using Nic.Galaxy.Domain.Service.Contract;
using Nic.Galaxy.Domain.ViewModel;
using WebApi.OutputCache.V2;

namespace Nic.Galaxy.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("")]
    public class WeatherController : ApiController
    {
        /// <summary>
        /// Gets or sets the galaxy service.
        /// </summary>
        /// <value>
        /// The galaxy service.
        /// </value>
        protected IGalaxyService GalaxyService { get; set; }

        /// <summary>
        /// Crea los próximo 10 años de las condiciones climática de la galáxia.
        /// </summary>
        /// <param name="forzar">Si setea en <c>true</c> fuerza e inicia todo nuevamente. Si se setea en <c>false</c>, revisa que se encuentre todos los días, sino los vuelve a generar</param>
        /// <returns></returns>
        [Route("iniciar/{forzar:bool}")]
        [HttpGet]
        public string Inizializate(bool forzar)
        {
            Domain.Data.SessionFactory.Transaction.ExecuteInTransaction(() =>
            {
                GalaxyService.Initialize(forzar);
            });
            return "Operación realizado con éxito";
        }


        /// <summary>
        /// Trae el clima de un dia específico.
        /// </summary>
        /// <param name="planeta">El nombre del planeta</param>
        /// <param name="clima">El clima. Posibles parámetros:
        /// Normal, Sequia, Lluvia, Optimo</param>
        /// <returns></returns>
        [Route("planeta/{planeta}/estadistica/{clima}")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 120)]
        public ResponseWeatherStatistic GetWeatherStatistic(string planeta, string clima)
        {
            ResponseWeatherStatistic responseWeatherStatistic = null;

            Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
            {
                responseWeatherStatistic = GalaxyService.WeatherStatistic(planeta, clima);
            });

            return responseWeatherStatistic;
        }

        /// <summary>
        /// Trae el clima de un dia específico del planeta Vulcano.
        /// </summary>
        /// <param name="clima">El clima. Posibles parámetros:
        /// Normal, Sequia, Lluvia, Optimo</param>
        /// <returns></returns>
        [Route("estadistica/{clima}")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 120)]
        public ResponseWeatherStatistic GetVulcanoWeatherStatistic(string clima)
        {
            ResponseWeatherStatistic responseWeatherStatistic = null;

            Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
            {
                responseWeatherStatistic = GalaxyService.WeatherStatistic("vulcano", clima);
            });

            return responseWeatherStatistic;
        }

        /// <summary>
        /// Trae el clima de un dia específico.
        /// </summary>
        /// <param name="dia">The day.</param>
        [Route("clima")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan=60, ServerTimeSpan=120)]
        public ResponseWeather GetWeatherDay(int dia)
        {
            ResponseWeather responseWeather = null;

            Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
            {
                responseWeather = GalaxyService.GetWeatherByDay(dia);
            });

            return responseWeather;
        }
    }
}