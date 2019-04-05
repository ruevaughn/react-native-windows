using System;
using System.Diagnostics;
using ReactNative.Net46.Events;
using ReactNative.Net46.Modules.Logging;

namespace ReactNative.Net46.Tracing.CustomTraceListeners
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
            LoggingModule.RegisterTraceListener(this);
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
