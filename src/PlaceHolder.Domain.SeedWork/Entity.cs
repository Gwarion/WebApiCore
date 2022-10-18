using System;
using System.Collections.Generic;
using System.Text;

namespace PlaceHolder.Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual Guid Guid { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
