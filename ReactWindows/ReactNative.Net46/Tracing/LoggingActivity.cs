using System;
using System.Diagnostics;

namespace ReactNative.Tracing
{
    /// <summary>
    /// Describes a logging activity instance
    /// </summary>
    public sealed class LoggingActivity : IDisposable
    {
        private  TraceSource _target;
        //private TraceSource traceSource;
        private readonly int _tag;
        private readonly string _eventName;
        private readonly SourceLevels _level;
        private readonly TraceWriter _traceWriter;
        private readonly LoggingFields LoggingFields;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingActivity"/> class.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        /// <param name="eventName"></param>
        /// <param name="level"></param>
        /// <param name="traceWriter"></param>
        /// <param name="loggingFields"></param>
        public LoggingActivity(TraceSource target, int tag, string eventName, SourceLevels level, TraceWriter traceWriter, LoggingFields loggingFields)
        {
            _target = target;
            _tag = tag;
            _eventName = eventName;
            _level = level;
            _traceWriter = traceWriter;
            this.LoggingFields = loggingFields;
            if (this.IsEnabled())
            {
                _traceWriter?.TraceWrite(_tag, _eventName, $"{TraceEventType.Start}{Environment.NewLine}{this.GetFieldsString()}");
            }
        }

        private string GetFieldsString()
        {
            return this.LoggingFields.IsEmpty ? String.Empty : $"Fields:{Environment.NewLine}{LoggingFields}";
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.IsEnabled())
            {
                _traceWriter?.TraceWrite(_tag, _eventName, TraceEventType.Stop.ToString());
            }
        }

        /// <summary>
        /// Gets a value indicating whetehr logging is enabled for the current TraceSource switch level
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled()
        {
            switch (_target.Switch.Level)
            {
                case SourceLevels.Off:
                    return false;
                case SourceLevels.All:
                    return true;
            }
            return _target.Switch.Level >= _level;
        }
    }
}
