using System;
using Nic.Galaxy.Domain.Entity.Base;
using Nic.Galaxy.Domain.Enum;

namespace Nic.Galaxy.Domain.Entity.Galaxy
{
    public class WeatherForecast : EntityBase<int>, IComparable<WeatherForecast>
    {
        public virtual Galaxy Galaxy { get; set; }
        public virtual int Day { get; set; }
        public virtual Weather Weather { get; set; }
        public virtual double Perimeter { get; set; }

        public virtual int CompareTo(WeatherForecast other)
        {
            if (other.Perimeter > Perimeter)
            {
                return -1;
            }

            return Perimeter > other.Perimeter ? 1 : 0;
        }
    }
}
