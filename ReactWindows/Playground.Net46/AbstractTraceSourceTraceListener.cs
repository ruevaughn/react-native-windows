using log4net;
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace Playground.Net46
{
    public abstract class AbstractTraceSourceTraceListener: TraceListener
    {
        protected abstract string RollingFileAppenderName { get;}

        protected abstract string LogRecordHeader { get; }

        protected abstract ILog LogInctance { get;}
        protected abstract ILog LogInctance2 { get; }

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
        }

        protected AbstractTraceSourceTraceListener(string initializeData)
        {
            this.InitializeLogger(initializeData);
        }

        protected abstract void InitializeLogger(string loggingOutputDirectory);

        private string getEventCacheString(TraceEventCache eventCache, TraceEventType eventType)
        {
            return 
                (eventType <= TraceEventType.Error
                ? $"Call stack: {eventCache.Callstack}{Environment.NewLine}{Environment.NewLine}"
                : String.Empty);
        }

        private void SaveToLogXml(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            var eventCacheString = this.getEventCacheString(eventCache, eventType);

            XDocument xDoc = new XDocument(
                    new XElement(this.LogRecordHeader,
                         new XElement("Time", eventCache.DateTime.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff")),
                         new XElement("ProcessId", eventCache.ProcessId),
                         new XElement("Source", source),
                         new XElement("EventType", eventType),
                         new XElement("EventCache", eventCacheString),
                         new XElement("Data", data)
                    )
                );

            this.Log(eventType, xDoc.ToString());
        }

        private void Log(TraceEventType eventType, string message)
        {
            switch (eventType)
            {
                case TraceEventType.Information:
                    LogInctance?.Info(message);
                    LogInctance2?.Info(message);
                    break;
                case TraceEventType.Error:
                    LogInctance?.Error(message);
                    LogInctance2?.Error(message);
                    break;
                default:
                    LogInctance?.Info(message);
                    break;
            }
        }

        private void SaveToLog(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
           string msg = $"Source: {source}{Environment.NewLine}"+
                        this.getEventCacheString(eventCache, eventType)+
                        $"EventType: {eventType}{Environment.NewLine}Data: {data}{Environment.NewLine}{Environment.NewLine}";

            this.Log(eventType, msg);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            this.SaveToLogXml(eventCache, source, eventType, id, data);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if (data.Length > 0)
            {
                this.SaveToLogXml(eventCache, source, eventType, id, String.Join(" | ", data));
            }
        }

    }
}
