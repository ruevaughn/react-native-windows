using System;
using Newtonsoft.Json.Linq;
using ReactNative.Bridge;
using ReactNative.Tracing;

namespace ReactNative.Net46.Modules.Logging
{
    /// <summary>
    /// Raises an events to RN
    /// </summary>
    public class LogsEventAggregator
    {
        private readonly ReactContext _context;
        private readonly ReactNativeLogTraceListener _traceListener;

        /// <summary>
        /// Initializes an instance
        /// </summary>
        /// <param name="context">current react context</param>
        /// <param name="traceListener">trace listener</param>
        public LogsEventAggregator(ReactContext context, ReactNativeLogTraceListener traceListener)
        {
            this._context = context;
            this._traceListener = traceListener;

            SubscribeToTraceListenerEvents();
        }

        private void SubscribeToTraceListenerEvents()
        {
            if (_traceListener != null)
            {
                this.UnsubscribeFromTraceListenerEvents();

                _traceListener.LogMessageCreated += LogMessageCreated;
            }
        }

        private void UnsubscribeFromTraceListenerEvents()
        {
            if (_traceListener != null)
            {
                // ReSharper disable once DelegateSubtraction
                _traceListener.LogMessageCreated -= LogMessageCreated;
            }
        }

        private void LogMessageCreated(object sender, LoggingEventArgs e)
        {
            if (_context != null && _context.HasActiveReactInstance)
            {
                var message = new JObject
                {
                    ["messageSender"] = $"{nameof(LogsEventAggregator)}",
                    ["message"] = e.Message
                };

                new LogEvent("logMessageCreated").Dispatch(this._context, message);
            }
        }
    }
}
