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
            int runSuccess = 0;
            int day;
            if (!int.TryParse(param, out day))
            {
                return;
            }
            Console.WriteLine("Iniciando y buscando clima... \n");
            var applicationContext = ContextRegistry.GetContext();
            var galaxyService = (IGalaxyService)applicationContext.GetObject("IGalaxyService");
            try
            {
                Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
                {
                    var responseWeather = galaxyService.GetWeatherByDay(day);
                    Console.WriteLine(responseWeather.Clima);
                });
            }
            catch (ValidException validException)
            {
                Console.WriteLine("{0}", validException.Message);
                runSuccess = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error al intentar calcular las estadísticas: {0}", ex.Message);
                runSuccess = -1;
            }
            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
            Environment.Exit(runSuccess);
        }

        private static void WeatherStatistic(string planetName, string param)
        {
            int runSuccess = 0;
            Console.WriteLine("Iniciando y buscando estadística del clima {0} para el planeta{1}... \n", param, planetName);
            var applicationContext = ContextRegistry.GetContext();
            var galaxyService = (IGalaxyService)applicationContext.GetObject("IGalaxyService");
            try
            {
                Domain.Data.SessionFactory.Transaction.ExecuteInSession(() =>
                {
                    var responseWeatherStatistic  = galaxyService.WeatherStatistic(planetName, param);
                    Console.WriteLine(responseWeatherStatistic.Text);
                });
            }
            catch (ValidException validException)
            {
                Console.WriteLine("{0}", validException.Message);
                runSuccess = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error al intentar calcular las estadísticas: {0}", ex.Message);
                runSuccess = -1;
            }
            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
            Environment.Exit(runSuccess);
        }

        private static void Inizialie(string param)
        {
            int runSuccess = 0;
            bool force;
            if (!bool.TryParse(param, out force))
            {
                return;
            }
            Console.WriteLine("Iniciando y generando dias climáticos... \n");
            var applicationContext = ContextRegistry.GetContext();
            var galaxyService = (IGalaxyService)applicationContext.GetObject("IGalaxyService");
            try
            {
                Domain.Data.SessionFactory.Transaction.ExecuteInTransaction(() =>
                {
                    galaxyService.Initialize(force);
                });
                Console.WriteLine("Proceso finalizado correctamente");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error al intentar calcular las estadísticas: {0}", ex.Message);
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
