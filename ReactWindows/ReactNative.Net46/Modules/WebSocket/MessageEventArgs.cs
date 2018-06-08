using System;

namespace ReactNative.Modules.WebSocket
{
    /// <summary>
    /// Represents the event data for the <see cref="IWebSocketClient.OnMessage"/> event.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        #region Internal Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"><see cref="string"/></param>
        public MessageEventArgs(string message)
        {
            this.Data = message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the message data as a <see cref="string"/>.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that represents the message data if its type is
        /// text or ping and if decoding it to a string has successfully done;
        /// otherwise, <see langword="null"/>.
        /// </value>
        public string Data { get; private set; }

        #endregion
    }
}
