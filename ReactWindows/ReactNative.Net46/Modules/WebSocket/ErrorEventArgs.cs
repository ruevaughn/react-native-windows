using System;

namespace ReactNative.Modules.WebSocket
{
    /// <summary>
    /// Represents the event data for the <see cref="IWebSocketClient.OnError"/> event.
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        #region Internal Constructors

        internal ErrorEventArgs(string message)
          : this(message, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ErrorEventArgs(string message, Exception exception)
        {
            this.Message = message;
            this.Exception = exception;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the exception that caused the error.
        /// </summary>
        /// <value>
        /// An <see cref="System.Exception"/> instance that represents the cause of
        /// the error if it is due to an exception; otherwise, <see langword="null"/>.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that represents the error message.
        /// </value>
        public string Message { get; private set; }

        #endregion
    }
}