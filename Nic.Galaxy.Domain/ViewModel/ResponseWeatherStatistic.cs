using System.Collections.Generic;
using System.Linq;

namespace Nic.Galaxy.Domain.ViewModel
{
    public class ResponseWeatherStatistic
    {
        public string Clima { get; set; }
        public int TotalPeriodo { get; set; }
        public IList<int> DiasMaximoLluvia { get; set; }

        public string Text
        {
            get
            {
                var maxDay = string.Empty;
                if (DiasMaximoLluvia != null && DiasMaximoLluvia.Any())
                {
                    maxDay = string.Format(", el/los día(s) {0} es de mayor intensidad", string.Join(", ", DiasMaximoLluvia));
                }

                return string.Format("{0} días de {1}{2}", TotalPeriodo, Clima, maxDay);
            }
        }
    }
}
