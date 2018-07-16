using System;
using System.Diagnostics;

namespace ReactNative.Tracing
{
    /// <summary>
    /// Describes a logging activity instance
    /// </summary>
    public sealed class LoggingActivity : IDisposable
    {
        private readonly TraceSource _target;
        private readonly int _tag;
        private readonly string _eventName;
        private readonly SourceLevels _level;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingActivity"/> class.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="tag"></param>
        /// <param name="eventName"></param>
        /// <param name="level"></param>
        public LoggingActivity(TraceSource target, int tag, string eventName, SourceLevels level)
        {
            _target = target;
            _tag = tag;
            _eventName = eventName;
            _level = level;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.IsEnabled())
            {
                _target.TraceEvent(TraceEventType.Information, _tag, _eventName);
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
