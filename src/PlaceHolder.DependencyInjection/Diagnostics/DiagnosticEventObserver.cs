using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.DependencyInjection.Diagnostics
{
    internal class DiagnosticEventObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly IEnumerable<IDiagnosticListener> _listeners;

        public string CategoryName { get; }

        public DiagnosticEventObserver(string categoryName, IEnumerable<IDiagnosticListener> listeners)
        {
            CategoryName = categoryName;
            _listeners = listeners;
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            var listener = _listeners.SingleOrDefault(l => l.EventName == value.Key);

            listener?.Handle(value.Value);
        }

        public void OnCompleted() { }
        public void OnError(Exception error) { }

    }
}
