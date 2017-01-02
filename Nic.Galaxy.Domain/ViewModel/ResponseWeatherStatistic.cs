namespace Nic.Galaxy.Domain.ViewModel
{
    public class ResponseWeatherStatistic
    {
        public string Clima { get; set; }
        public int TotalPeriodo { get; set; }
        public int DiaMaximoLluvia { get; set; }

        public string Text
        {
            get
            {
                var maxDay = string.Empty;
                if (DiaMaximoLluvia >= 0)
                {
                    maxDay = string.Format(", el día {0} es de mayor intensidad", DiaMaximoLluvia);
                }

                return string.Format("{0} días de {1}{2}", TotalPeriodo, Clima, maxDay);
            }
        }
    }
}
