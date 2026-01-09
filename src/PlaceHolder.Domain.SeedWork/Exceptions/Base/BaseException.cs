using System;
using System.Net;

namespace PlaceHolder.Domain.SeedWork.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        public virtual HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        protected BaseException() { }

        protected BaseException(string message) : base(message) { }
    }
}
