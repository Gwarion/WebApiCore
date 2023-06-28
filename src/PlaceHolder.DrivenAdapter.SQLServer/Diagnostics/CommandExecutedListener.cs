using PlaceHolder.DependencyInjection.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prometheus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace PlaceHolder.DrivenAdapter.SQLServer.Diagnostics
{
    internal class CommandExecutedListener : IDiagnosticListener
    {
        private readonly Histogram _executionTimeHistogram;

        public string CategoryName => DbLoggerCategory.Name;

        public string EventName => RelationalEventId.CommandExecuted.Name;

        public CommandExecutedListener()
        {
            _executionTimeHistogram = Metrics.CreateHistogram(
                "DB_QUERY_DURATION",
                "Execution time of a database query",
                new HistogramConfiguration
                {
                    Buckets = Histogram.ExponentialBuckets(0.01, 2, 10),
                    LabelNames = new[] { "Method_Name" }
                });
        }
        public void Handle(object eventData)
        {
            if(eventData is CommandExecutedEventData data)
            {
                _executionTimeHistogram
                    .Labels(ExtractTag(data.Command))
                    .Observe(data.Duration.TotalMilliseconds);
            };
        }

        private string ExtractTag(DbCommand command)
        {
            const string separator = "--";

            if (!command.CommandText.StartsWith(separator)) return string.Empty;

            return command.CommandText
                .Split(Environment.NewLine)
                .First()
                .Replace(separator, string.Empty)
                .Trim();
        }
    }
}
