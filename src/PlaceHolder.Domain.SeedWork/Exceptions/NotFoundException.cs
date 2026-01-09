using PlaceHolder.Domain.SeedWork.Exceptions.Base;
using System.Net;

namespace PlaceHolder.Domain.SeedWork.Exceptions
{
    public abstract class NotFoundException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        protected NotFoundException(string message) : base(message) { }
    }
}
