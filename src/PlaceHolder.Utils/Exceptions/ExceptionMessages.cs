using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Utils.Exceptions
{
    public static class ExceptionMessages
    {
        public static readonly string InternalServerExceptionMessage = "Internal Server error";
        public static readonly string NotImplementedExceptionMessage = "Feature is currently not supported";
        public static readonly string NotFoundExceptionMessage = "Requested item was not found";
        public static readonly string ValueObjectValidationExceptionMessage = "Invalid Request data";
        public static readonly string InvalidConfigurationExceptionMessage= "Missing or invalid configuration";
    }
}
