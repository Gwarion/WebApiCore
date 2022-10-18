using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Utils.Exceptions.TechnicalExceptions
{
    public class TechnicalException : Exception
    {
        public TechnicalException(string message) : base(message) { }
    }
}
