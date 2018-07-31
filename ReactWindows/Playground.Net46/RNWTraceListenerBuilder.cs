using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Net46
{
    public class RNWTraceListenerBuilder
    {
        private readonly string TracerClassName;
        private readonly SourceLevels TraceSourceLevel;

        public RNWTraceListenerBuilder(string tracerClassName, SourceLevels traceSourceLevel)
        {
            this.TracerClassName = tracerClassName;
            this.TraceSourceLevel = traceSourceLevel;
        }

        public RNWTraceListener Build()
        {
            TraceSource source = this.GetTraceSource();
            RNWTraceListener rnwListener = new RNWTraceListener(new CustomLogFile("RNWTrace.log"));
            source.Listeners.Add(rnwListener);
            Trace.AutoFlush = true;
            return rnwListener;
        }

        private TraceSource GetTraceSource()
        {
            var tracerClass =
               AppDomain.CurrentDomain.GetAssemblies()
                      .SelectMany(t => t.GetTypes())
                      .Where(t => t.IsClass && t.Name == this.TracerClassName).FirstOrDefault();

            if (tracerClass == null)
            {
                throw new ArgumentException($"Can't find {this.TracerClassName} type");
            }

            var traceSourceInstance = tracerClass.GetProperties().Where(p => p.Name == "Instance").FirstOrDefault();

            if (traceSourceInstance == null)
            {
                throw new ArgumentException($"Can't find TraceSource property in {this.TracerClassName}");
            }

            TraceSource source = traceSourceInstance.GetValue(traceSourceInstance.Name) as TraceSource;

            if (source == null)
            {
                throw new ArgumentException($"TraceSource property is not a TraceSource type");
            }
            source.Listeners.Clear();
            source.Switch.Level = this.TraceSourceLevel;
            return source;
        }
    }
}
