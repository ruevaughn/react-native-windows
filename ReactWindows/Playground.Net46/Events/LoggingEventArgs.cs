using System;

namespace Playground.Net46.Events
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
