using System;

namespace ReactNative.Net46.Modules.Logging
{
    /// <summary>
    /// Logs event args
    /// </summary>
    public class LoggingEventArgs : EventArgs
    {
        /// <inheritdoc />
        public LoggingEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Log message
        /// </summary>
        public string Message { get; }
    }
}
