using System.Collections.Generic;
using Nic.Galaxy.Domain.Entity.Base;

namespace Nic.Galaxy.Domain.Entity.Galaxy
{
    public class Galaxy : EntityBaseName<int>
    {
        private IList<Planet> _planets;

        public virtual IList<Planet> Planets
        {
            get { return _planets ?? new List<Planet>(0); }
            set { _planets = value; }
        } 
    }
}
