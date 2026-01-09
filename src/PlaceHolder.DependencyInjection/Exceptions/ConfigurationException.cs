using System;

namespace PlaceHolder.DependencyInjection.Exceptions
{
    internal class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }
}
