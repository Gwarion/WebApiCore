using System;

namespace PlaceHolder.Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual Guid Guid { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
