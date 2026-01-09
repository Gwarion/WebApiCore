using PlaceHolder.Domain.SeedWork.Exceptions;
using System;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Exceptions
{
    public class ConsumerNotFoundException : NotFoundException
    {
        public ConsumerNotFoundException(Guid id) : base($"Consumer with id {id} was not found.") { }
    }
}
