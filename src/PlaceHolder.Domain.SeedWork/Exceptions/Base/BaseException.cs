using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.SeedWork.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        public virtual HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        protected BaseException() { }

        protected BaseException(string message) : base(message) { }
    }
}
