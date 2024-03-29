using PlaceHolder.Domain.SeedWork.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions
{
    public class ConsumerNotFoundException : NotFoundException
    {
        public ConsumerNotFoundException(Guid id) : base($"Consumer with id {id} was not found.") { }
    }
}
