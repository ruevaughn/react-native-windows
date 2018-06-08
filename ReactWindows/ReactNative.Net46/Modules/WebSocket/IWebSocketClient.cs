using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNative.Modules.WebSocket
{
    /// <summary>
    /// Interface that describes WebSocket client
    /// </summary>
    public interface IWebSocketClient
    {
        #region Events
        /// <summary>
        /// Occurs when the WebSocket receives a message.
        /// </summary>
        event EventHandler<MessageEventArgs> OnMessage;
        /// <summary>
        /// Occurs when the WebSocket connection has been established.
        /// </summary>
        event EventHandler OnOpen;

        /// <summary>
        /// Occurs when the WebSocket gets an error.
        /// </summary>
        event EventHandler<ErrorEventArgs> OnError;

        /// <summary>
        /// Occurs when the WebSocket connection has been closed.
        /// </summary>
        event EventHandler<CloseEventArgs> OnClose;
        #endregion

        #region Properties
        /// <summary>
        /// Sets Origin header of HTTP request
        /// </summary>
        string Origin { set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sends message to the server
        /// </summary>
        /// <remarks>
        /// This method does not wait for the send to be complete.
        /// </remarks>
        /// <param name="data"><see cref="string"/> that contains message </param>
        void SendAsync(string data);

        /// <summary>
        /// Sends message to the server
        /// </summary>
        /// <remarks>
        /// This method does not wait for the send to be complete.
        /// </remarks>
        /// <param name="data">array of data to send</param>
        void SendAsync(byte[] data);

        /// <summary>
        /// Establishes a connection
        /// <param name="url">URI of websocket server</param>
        /// </summary>
        void Connect(string url);

        /// <summary>
        /// Closes the connection with the specified <paramref name="code"/>.
        /// </summary>
        /// <param name="code">status code indicating the reason for the close.</param>
        /// <param name="reason">reason of connection close</param>
        void Close(ushort code, string reason);
        #endregion
    }
}
