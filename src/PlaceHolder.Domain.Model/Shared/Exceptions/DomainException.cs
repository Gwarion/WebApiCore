using System;

namespace PlaceHolder.Domain.Model.Shared.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }
}
