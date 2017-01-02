using System;

namespace Nic.Galaxy.Domain.Entity.Base
{
    public abstract class EntityBase<T>
    {
        public virtual T Id { get; set; }
    }
}
