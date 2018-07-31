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
        private readonly TraceWriter _traceWriter;

        private LoggingFields _fields;

        public LoggingActivityBuilder(TraceSource target, int tag, string eventName, SourceLevels level, TraceWriter traceWriter)
        {
            _target = target;
            _tag = tag;
            _eventName = eventName;
            _level = level;
            _traceWriter = traceWriter;
            _fields = new LoggingFields();
        }

        public IDisposable Start()
        {
            return new LoggingActivity(_target, _tag, _eventName, _level, _traceWriter, _fields);
        }

        public LoggingFields Fields
        {
            get
            {
                return _fields;
            }
        }
    }
}
