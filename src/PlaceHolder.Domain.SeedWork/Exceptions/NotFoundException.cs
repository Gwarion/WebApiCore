using PlaceHolder.Domain.SeedWork.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.SeedWork.Exceptions
{
    public abstract class NotFoundException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        protected NotFoundException(string message) : base(message) { }
    }
}
