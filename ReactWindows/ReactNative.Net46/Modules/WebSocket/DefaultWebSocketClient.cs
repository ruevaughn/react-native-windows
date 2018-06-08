using System;
using WebSocketSharp;

namespace ReactNative.Modules.WebSocket
{
    internal class DefaultWebSocketClient : IWebSocketClient
    {
        private WebSocketSharp.WebSocket webSocket;

        #region IWebSocketClient
        public string Origin
        {
            set
            {
                webSocket.Origin = value;
            }
        }

        public event EventHandler<CloseEventArgs> OnClose;
        public event EventHandler<ErrorEventArgs> OnError;
        public event EventHandler<MessageEventArgs> OnMessage;
        public event EventHandler OnOpen;

        public void Connect(string url)
        {
            this.webSocket = new WebSocketSharp.WebSocket(url);
            this.webSocket.OnClose += WebSocket_OnClose;
            this.webSocket.OnError += WebSocket_OnError;
            this.webSocket.OnMessage += WebSocket_OnMessage;
            this.webSocket.OnOpen += WebSocket_OnOpen;

            this.webSocket.Connect();
        }

        public void SendAsync(byte[] data)
        {
            this.webSocket?.SendAsync(data, null);
        }

        public void SendAsync(string data)
        {
            webSocket?.SendAsync(data, null);
        }

        public void Close(ushort code, string reason)
        {
            webSocket?.Close(code, reason);
        }

        #endregion

        #region private methods

        private void WebSocket_OnOpen(object sender, EventArgs e)
        {
            this.OnOpen?.Invoke(sender, EventArgs.Empty);
        }

        private void WebSocket_OnMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {
            this.OnMessage?.Invoke(this, new MessageEventArgs(e.Data));
        }

        private void WebSocket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            this.OnError?.Invoke(this, new ErrorEventArgs(e.Message, e.Exception));
        }

        private void WebSocket_OnClose(object sender, WebSocketSharp.CloseEventArgs e)
        {
            this.OnClose?.Invoke(this, new CloseEventArgs(e.Code, e.Reason));
        }

        #endregion
    }
}
