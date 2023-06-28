using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.DependencyInjection.Diagnostics
{
    internal class DiagnosticObserver : IObserver<DiagnosticListener>
    {
        private readonly IEnumerable<DiagnosticEventObserver> _observers;

        public DiagnosticObserver(IEnumerable<DiagnosticEventObserver> observers)
        {
            _observers = observers;
        }

        public void OnNext(DiagnosticListener value)
        {
            var listener = _observers.SingleOrDefault(l => l.CategoryName == value.Name);

            if (listener != null)
            {
                value.Subscribe(listener);
            }
        }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
    }
}
