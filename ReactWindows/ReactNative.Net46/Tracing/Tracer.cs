#define TRACE

using System;
using System.Diagnostics;

namespace ReactNative.Tracing
{
    /// <summary>
    /// Temporary NullTracing helpers for the application.
    /// </summary>
    internal static class Tracer
    {
        /// <summary>
        /// Trace ID for bridge events.
        /// </summary>
        public const int TRACE_TAG_REACT_BRIDGE = 0;

        /// <summary>
        /// Trace ID for application events.
        /// </summary>
        public const int TRACE_TAG_REACT_APPS = 1;

        /// <summary>
        /// Trace ID for view events.
        /// </summary>
        public const int TRACE_TAG_REACT_VIEW = 2;

        /// <summary>
        /// The logging channel instance.
        /// </summary>
        public static TraceSource Instance { get; } = new TraceSource("ReactWindows", SourceLevels.Information);

        public static LoggingActivityBuilder EmptyLoggingActivityBuilder;

        /// <summary>
        /// Create a logging activity builder.
        /// </summary>
        /// <param name="tag">The trace tag.</param>
        /// <param name="eventName">The event name.</param>
        /// <returns>The null logging activity builder with a fake Start method.</returns>
        public static LoggingActivityBuilder Trace(int tag, string eventName)
        {
            if (Instance.Switch.Level != SourceLevels.Off)
            {
                return new LoggingActivityBuilder(Instance, tag, eventName, SourceLevels.Information);
            }

            return EmptyLoggingActivityBuilder ??
                   (EmptyLoggingActivityBuilder = new LoggingActivityBuilder(Instance, 0, "None", SourceLevels.Off));
        }

        /// <summary>
        /// Write an event.
        /// </summary>
        /// <param name="tag">The trace tag.</param>
        /// <param name="eventName">The event name.</param>
        public static void Write(int tag, string eventName)
        {
            Instance.TraceData(TraceEventType.Information, tag, eventName, tag);
        }

        /// <summary>
        /// Write an error event.
        /// </summary>
        /// <param name="tag">The trace tag.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="ex">The exception.</param>
        public static void Error(int tag, string eventName, Exception ex)
        {
            Instance.TraceData(TraceEventType.Error, tag, ex != null
                ? $"{eventName} - {ex}"
                : $"{eventName}",
                tag);
        }
    }
}