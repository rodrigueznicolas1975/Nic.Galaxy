using System;
using NDesk.Options;
using Nic.Galaxy.Domain.Exception;
using Nic.Galaxy.Domain.Service.Contract;
using Spring.Context.Support;

namespace Nic.Galaxy
{
    static class Program
    {
        private static OptionSet _optionSet;
        static void Main(string[] args)
        {
            _optionSet = new OptionSet
            {
                {"i|iniciar=", "Crea los próximo 10 años de las condiciones climática de la galáxia. Valores posibles de parámetros: true=Forzar inicialización, false=verifica si faltan datos, y en caso de faltar, fuerza inicialización.", Inizialie},
                {"ev|evulcano=", "Trae la cantidad de período del clima específico del planeta vulcano. Posibles parámetros: Normal, Sequia, Lluvia, Optimo", x=> WeatherStatistic("vulcano", x)},
                {"ef|eferengi=", "Trae la cantidad de período del clima específico del planeta vulcano. Posibles parámetros: Normal, Sequia, Lluvia, Optimo", x=> WeatherStatistic("ferengi", x)},
                {"eb|ebetasoide=", "Trae la cantidad de período del clima específico del planeta vulcano. Posibles parámetros: Normal, Sequia, Lluvia, Optimo", x=> WeatherStatistic("betasoide", x)},
                {"c|clima=", "Trae el clima de un dia específico. Parámetro numérico", GetWeatherDay},
                { "?|h|help", "Muestra esta ayuda y sale", x => { }}
            };

            _optionSet.Parse(args);
            ShowHelp();
        }

        private static void GetWeatherDay(string param)
        {
            int day;
            if (!int.TryParse(param, out day))
            {
                return;
            }
            ExecuteProcess(string.Format("buscar clima del día {0}", day), () =>
            {
                Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
                {
                    var responseWeather = _galaxyService.GetWeatherByDay(day);
                    Console.WriteLine("El clima del día {0} es: {1}", day, responseWeather.Clima);
                });

            });
        }

        private static void WeatherStatistic(string planetName, string param)
        {
            ExecuteProcess(string.Format("estadística del clima {0} para el planeta{1}", param, planetName), () =>
            {
                Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
                {
                    var responseWeatherStatistic = _galaxyService.WeatherStatistic(planetName, param);
                    Console.WriteLine(responseWeatherStatistic.Text);
                });
            });
        }

        private static void Inizialie(string param)
        {
            bool force;
            if (!bool.TryParse(param, out force))
            {
                return;
            }
            ExecuteProcess("de generación de los días climáticos", () =>
            {
                Domain.Data.SessionFactory.Transaction.ExecuteInTransaction(() =>
                {
                    _galaxyService.Initialize(force);
                });
                Console.WriteLine("Proceso finalizado correctamente");
            });
        }

        private static IGalaxyService _galaxyService;

        private static void ExecuteProcess(string processDescription, Action action)
        {
            int runSuccess = 0;
            Console.WriteLine("Iniciando proceso {0}...", processDescription);
            var applicationContext = ContextRegistry.GetContext();
            _galaxyService = (IGalaxyService)applicationContext.GetObject("IGalaxyService");
            try
            {
                action.Invoke();
            }
            catch (ValidException validException)
            {
                Console.WriteLine("{0}", validException.Message);
                runSuccess = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error al intentar ejecutar el proceso: {0}", ex.Message);
                runSuccess = -1;
            }
            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
            Environment.Exit(runSuccess);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Modo de uso: Nic.Galaxy.exe [OPCIONES]");
            Console.WriteLine();
            Console.WriteLine("Opciones:");
            _optionSet.WriteOptionDescriptions(Console.Out);
            Console.ReadKey();
        }
    }
}
