using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.DependencyInjection.Diagnostics
{
    public interface IDiagnosticListener
    {
        string CategoryName { get; }
        string EventName { get; }

        public void Handle(object eventData);
    }
}
