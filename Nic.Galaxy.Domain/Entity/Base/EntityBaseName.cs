using System;

namespace Nic.Galaxy.Domain.Entity.Base
{
    public abstract class EntityBaseName<T> : EntityBase<T>
    {
        public virtual string Name { get; set; }
    }
}
