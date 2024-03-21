using System;

namespace PlaceHolder.Domain.Model.Shared.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(Guid id) 
            : base($"entity with id {id} was not found") { }
    }
}
