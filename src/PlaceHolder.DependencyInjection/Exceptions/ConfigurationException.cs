using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.DependencyInjection.Exceptions
{
    internal class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }
}
