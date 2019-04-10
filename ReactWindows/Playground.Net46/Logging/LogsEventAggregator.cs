using Newtonsoft.Json.Linq;
using Playground.Net46.Events;
using ReactNative.Bridge;

namespace Playground.Net46.Logging
{
    /// <summary>
    /// Raises an events to RN
    /// </summary>
    public static class LogsEventAggregator
    {
        private static ReactContext _context;
        private static ReactNativeLogTraceListener _traceListener;

        public static void RegisterReactContext(ReactContext context)
        {
            _context = context;
        }

        private static void SubscribeToTraceListenerEvents()
        {
            if (_traceListener != null)
            {
                UnsubscribeFromTraceListenerEvents();

                _traceListener.LogMessageCreated += LogMessageCreated;
            }
        }

        private static void UnsubscribeFromTraceListenerEvents()
        {
            if (_traceListener != null)
            {
                // ReSharper disable once DelegateSubtraction
                _traceListener.LogMessageCreated -= LogMessageCreated;
            }
        }

        private static void LogMessageCreated(object sender, LoggingEventArgs e)
        {
            if (_context != null && _context.HasActiveReactInstance)
            {
                var message = new JObject
                {
                    ["messageSender"] = $"{nameof(LogsEventAggregator)}",
                    ["message"] = e.Message
                };

                new LogEvent("logMessageCreated").Dispatch(_context, message);
            }
        }

        public static void RegisterTraceListener(ReactNativeLogTraceListener traceListener)
        {
            _traceListener = traceListener;
            SubscribeToTraceListenerEvents();
        }
    }
}
