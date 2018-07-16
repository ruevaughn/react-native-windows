using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Runtime.InteropServices;

namespace ReactNative.Tracing
{
    internal class LoggingActivityBuilder
    {
        private readonly TraceSource _target;
        private readonly int _tag;
        private readonly string _eventName;
        private readonly SourceLevels _level;

        public LoggingActivityBuilder(TraceSource target, int tag, string eventName, SourceLevels level)
        {
            _target = target;
            _tag = tag;
            _eventName = eventName;
            _level = level;
        }

        public LoggingActivityBuilder With(string key, string value)
        {
            return this;
        }

        public LoggingActivityBuilder With(string key, int value)
        {
            return this;
        }

        public IDisposable Start()
        {
            return new LoggingActivity(_target, _tag, _eventName, _level);
        }
    }
}
