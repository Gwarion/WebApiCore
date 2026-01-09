using PlaceHolder.Domain.SeedWork.Exceptions.Base;

namespace PlaceHolder.Domain.Model.Shared.Exceptions
{
    public abstract class DomainException : BaseException
    {
        protected DomainException(string message) : base(message) { }
    }
}
