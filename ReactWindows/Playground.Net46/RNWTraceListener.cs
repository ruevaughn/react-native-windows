using log4net;
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace Playground.Net46
{
    public class RNWTraceListener : TraceListener
    {
        private const string RollingFileAppenderName = "RNWRollingFileAppender";

        private ILog _log;
        private string LoggingOutputDirectory = String.Empty;

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
        }

        public RNWTraceListener()
        {
            this.InitializeLogger();
        }

        private void InitializeLogger()
        {
            this.LoggingOutputDirectory = Properties.Settings.Default.LoggingOutputDirectory;

            this._log = LogManager.GetLogger("RNWLog");

            string path = String.Format("{0}-{1}-{2}-{3}{4}", "RNW", DateTime.Now.Month.ToString("00")
                                   , DateTime.Now.Day.ToString("00")
                                   , DateTime.Now.Year.ToString(), ".log");

            this.ChangeFilePath(RollingFileAppenderName, path);
        }

        private void ChangeFilePath(string appenderName, string newFilename)
        {
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetRepository();
            foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
            {
                if (appender.Name.CompareTo(appenderName) == 0 && appender is log4net.Appender.FileAppender)
                {
                    log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)appender;
                    fileAppender.File = System.IO.Path.Combine(String.IsNullOrEmpty(this.LoggingOutputDirectory) ? fileAppender.File : this.LoggingOutputDirectory, newFilename);
                    fileAppender.ActivateOptions();
                }
            }
        }

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
                    new XElement("RNWLog",
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
                    _log?.Info(message);
                    break;
                case TraceEventType.Error:
                    _log?.Error(message);
                    break;
                default:
                    _log?.Info(message);
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
