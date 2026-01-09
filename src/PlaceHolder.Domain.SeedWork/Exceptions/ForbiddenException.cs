using PlaceHolder.Domain.SeedWork.Exceptions.Base;
using System.Net;

namespace PlaceHolder.Domain.SeedWork.Exceptions
{
    internal class ForbiddenException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    }
}
