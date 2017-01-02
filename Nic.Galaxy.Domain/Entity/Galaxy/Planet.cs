using Nic.Galaxy.Domain.Entity.Base;

namespace Nic.Galaxy.Domain.Entity.Galaxy
{
    public class Planet : EntityBaseName<int>
    {
        public virtual int Velocity { get; set; }
        public virtual int Direction { get; set; }
        public virtual int Radius { get; set; }
        public virtual Galaxy Galaxy { get; set; }
    }
}
