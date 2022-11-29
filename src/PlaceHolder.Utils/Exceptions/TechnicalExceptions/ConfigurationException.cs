using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Utils.Exceptions.TechnicalExceptions
{
    public class ConfigurationException : TechnicalException
    {
        public ConfigurationException(string message) : base(message) { }
    }
}
