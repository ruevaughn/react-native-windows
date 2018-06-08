using System;

namespace ReactNative.Modules.WebSocket
{
    /// <summary>
    /// Represents the event data for the <see cref="IWebSocketClient.OnClose"/> event.
    /// </summary>
    public class CloseEventArgs : EventArgs
    {
        #region Internal Constructors

        internal CloseEventArgs(ushort code)
          : this(code, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="reason"></param>
        public CloseEventArgs(ushort code, string reason)
        {
            this.Code = code;
            this.Reason = reason;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the status code for the close.
        /// </summary>
        /// <value>
        /// A <see cref="ushort"/> that represents the status code for the close if any.
        /// </value>
        public ushort Code { get; private set; }

        /// <summary>
        /// Gets the reason for the close.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that represents the reason for the close if any.
        /// </value>
        public string Reason { get; private set; }

        #endregion
    }
}