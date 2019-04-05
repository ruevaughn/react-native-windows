using System;
using System.Diagnostics;
using Playground.Net46.Events;

namespace Playground.Net46.Logging
{
    /// <summary>
    /// Custom trace listener
    /// </summary>
    public class ReactNativeLogTraceListener: TraceListener
    {
        /// <summary>
        /// Initializes an instance
        /// </summary>
        /// <param name="name">listener name</param>
        public ReactNativeLogTraceListener(string name) : base(name)
        {
            LogsEventAggregator.RegisterTraceListener(this);
        }

        /// <summary>
        /// Calls LogMessageCreated with log message
        /// </summary>
        /// <param name="message">log message</param>
        public override void Write(string message)
        {
            LogMessageCreated?.Invoke(this, new LoggingEventArgs(message));
        }

        /// <summary>
        /// Calls LogMessageCreated with log message
        /// </summary>
        /// <param name="message">log message</param>
        public override void WriteLine(string message)
        {
            LogMessageCreated?.Invoke(this, new LoggingEventArgs(message));
        }

        /// <summary>
        /// Log message created event
        /// </summary>
        public EventHandler<LoggingEventArgs> LogMessageCreated;
    }
}
