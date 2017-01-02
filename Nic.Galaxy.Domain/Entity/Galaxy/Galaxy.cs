using System.Collections.Generic;
using Nic.Galaxy.Domain.Entity.Base;

namespace Nic.Galaxy.Domain.Entity.Galaxy
{
    public class Galaxy : EntityBaseName<int>
    {
        public virtual IList<Planet> Planets { get; set; } 
    }
}
