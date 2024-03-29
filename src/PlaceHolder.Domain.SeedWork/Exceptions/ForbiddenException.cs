using PlaceHolder.Domain.SeedWork.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.SeedWork.Exceptions
{
    internal class ForbiddenException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    }
}
