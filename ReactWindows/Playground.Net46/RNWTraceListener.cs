using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Net46
{
    public class RNWTraceListener : TraceListener
    {
        private readonly ICustomLogFile LogFile;

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {

        }

        public RNWTraceListener()
        {

        }

        public RNWTraceListener(ICustomLogFile logFile)
        {
            this.LogFile = logFile;
        }

        private string getEventCacheString(TraceEventCache eventCache, TraceEventType eventType)
        {
            return $"Time: {eventCache.DateTime}{Environment.NewLine}" +
                $"ProcessId: {eventCache.ProcessId}{Environment.NewLine}" +
                (eventType <= TraceEventType.Error
                ? $"Call stack: {eventCache.Callstack}{Environment.NewLine}{Environment.NewLine}"
                : String.Empty);
        }

        private void SaveToLog(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
           string msg = $"RNW trace data{Environment.NewLine}" +
                        $"Source: {source}{Environment.NewLine}"+
                        this.getEventCacheString(eventCache, eventType)+
                        $"EventType: {eventType}{Environment.NewLine}Data: {data}{Environment.NewLine}{Environment.NewLine}";
           this.LogFile?.WriteLog(msg);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            this.SaveToLog(eventCache, source, eventType, id, data);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if (data.Length > 0)
            {
                this.SaveToLog(eventCache, source, eventType, id, String.Join(Environment.NewLine, data));
            }
        }
    }
}
