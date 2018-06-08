using Newtonsoft.Json.Linq;
using ReactNative.Bridge;
using ReactNative.Common;
using ReactNative.Modules.Core;
using ReactNative.Tracing;
using System;
using System.Collections.Generic;


namespace ReactNative.Modules.WebSocket
{
    /// <summary>
    /// Modules that implements WebSocket client interface.
    /// </summary>
    public class WebSocketModule : ReactContextNativeModuleBase
    {
        private readonly IWebSocketClient _webSocketClient;
        private readonly IDictionary<int, IWebSocketClient> _webSocketConnections = new Dictionary<int, IWebSocketClient>();
        private readonly object locker = new object();

        #region Constructor(s)

        /// <summary>
        /// Instantiates the <see cref="WebSocketModule"/>.
        /// </summary>
        /// <param name="reactContext">The React context.</param>
        public WebSocketModule(ReactContext reactContext)
            : this(new DefaultWebSocketClient(), reactContext)
        {
        }

        /// <summary>
        /// Instantiates the <see cref="WebSocketModule"/>.
        /// </summary>
        /// <param name="webSocketClient">The websocket client implementation</param>
        /// <param name="reactContext">The React context.</param>
        public WebSocketModule(IWebSocketClient webSocketClient, ReactContext reactContext)
            : base(reactContext)
        {
            if (webSocketClient == null)
            {
                throw new ArgumentNullException(nameof(webSocketClient));
            }

            this._webSocketClient = webSocketClient;
        }

        #endregion

        #region NativeModuleBase Overrides
        
        /// <summary>
        /// Gets the name of the native module.
        /// </summary>
        public override string Name => "WebSocketModule";

        #endregion

        #region Public Methods

        /// <summary>
        /// Start connection to server
        /// </summary>
        /// <param name="url"><see cref="string"/> that repersent URL of the server</param>
        /// <param name="protocols">protocols that used (may be null)</param>
        /// <param name="options">options JSON based string that provides additional
        /// connection options, like Origin for example.</param>
        /// <param name="id">the ID of the socket is used to identify connection.</param>
        [ReactMethod]
        public void connect(string url, string[] protocols, JObject options, int id)
        {
            this._webSocketClient.OnMessage += (sender, args) =>
            {
                OnMessageReceived(id, sender, args);
            };

            this._webSocketClient.OnOpen += (sender, args) =>
            {
                OnOpen(id, this._webSocketClient, args);
            };

            this._webSocketClient.OnError += (sender, args) =>
            {
                OnError(id, args);
            };

            this._webSocketClient.OnClose += (sender, args) =>
            {
                OnClosed(id, sender, args);
            };

            try
            {
                InitializeInBackground(this._webSocketClient, url, options);
            }
            catch(Exception e) // in case if URL is not valid
            {
                OnError(id, new ErrorEventArgs("Cannot connect", e));
            }
        }

        /// <summary>
        /// Closes connection to websocket
        /// </summary>
        /// <param name="code">Code that will be sent to the server that indicates the reason of 
        /// disconnection.
        /// </param>
        /// <param name="reason"><see cref="string"/> that may be send to extend the reson. </param>
        /// <param name="id">the ID of the socket that will be closed</param>
        [ReactMethod]
        public void close(ushort code, string reason, int id)
        {
            IWebSocketClient webSocket;

            lock (this.locker)
            {
                if (!_webSocketConnections.TryGetValue(id, out webSocket))
                {
                    Tracer.Write(
                        ReactConstants.Tag,
                        $"Cannot close WebSocket. Unknown WebSocket id {id}.");

                    return;
                }
            }

            try
            {
                webSocket.Close(code, reason);
            }
            catch (Exception ex)
            {
                lock (this.locker)
                {
                    if (_webSocketConnections.ContainsKey(id))
                    {
                        _webSocketConnections.Remove(id);
                    }

                    Tracer.Error(
                        ReactConstants.Tag,
                        $"Could not close WebSocket connection for id '{id}'.",
                        ex);
                }
            }
        }

        /// <summary>
        /// Sends message to the server
        /// </summary>
        /// <param name="message"><see cref="string"/> that contains the message that should be sent.</param>
        /// <param name="id">the ID of the socket that will be used</param>
        [ReactMethod]
        public void send(string message, int id)
        {
            SendMessageInBackground(id, message);
        }

        #endregion

        #region Event Handlers

        private void OnOpen(int id, IWebSocketClient webSocket, EventArgs args)
        {
            if (webSocket != null)
            {
                lock (this.locker)
                {
                    _webSocketConnections.Add(id, webSocket);

                    SendEvent("websocketOpen", new JObject
                    {
                        {"id", id},
                    });
                }
            }
        }

        private void OnClosed(int id, object webSocket, CloseEventArgs args)
        {
            lock (this.locker)
            {
                if (_webSocketConnections.ContainsKey(id))
                {
                    _webSocketConnections.Remove(id);

                    SendEvent("websocketClosed", new JObject
                    {
                        {"id", id},
                        {"code", args.Code},
                        {"reason", args.Reason},
                    });
                }
                else
                {
                    SendEvent("websocketFailed", new JObject
                    {
                        { "id", id },
                        {"code", args.Code},
                        { "message", args.Reason },
                    });
                }
            }
        }

        private void OnError(int id, ErrorEventArgs args)
        {
            lock (this.locker)
            {
                if (_webSocketConnections.ContainsKey(id))
                {
                    _webSocketConnections.Remove(id);
                }
            }

            SendEvent("websocketFailed", new JObject
            {
                { "id", id },
                { "message", args.Message },
            });
        }

        private void OnMessageReceived(int id, object sender, MessageEventArgs args)
        {
            var message = args.Data;
            SendEvent("websocketMessage", new JObject
                {
                    { "id", id },
                    { "data", message },
                });
        }

        #endregion

        #region Private Methods

        private void InitializeInBackground(IWebSocketClient webSocket, string url, JObject options)
        {
            var parsedOptions = new WebSocketOptions(options);

            webSocket.Origin = parsedOptions.Origin;

            webSocket.Connect(url);
        }

        private void SendMessageInBackground(int id, string message)
        {
            IWebSocketClient webSocket;

            lock (this.locker)
            {
                if (!_webSocketConnections.TryGetValue(id, out webSocket))
                {
                    SendEvent("websocketFailed", new JObject
                    {
                        { "id", id },
                        { "message", $"Unknown WebSocket id {id}." },
                    });
                }
            }

            webSocket?.SendAsync(message);
        }

        private void SendMessageInBackground(int id, byte[] message)
        {
            IWebSocketClient webSocket;

            lock (this.locker)
            {
                if (!_webSocketConnections.TryGetValue(id, out webSocket))
                {
                    SendEvent("websocketFailed", new JObject
                    {
                        { "id", id },
                        { "message", $"Unknown WebSocket id {id}." },
                    });
                }
            }

            webSocket?.SendAsync(message);
        }

        private void SendEvent(string eventName, JObject parameters)
        {
            Context.GetJavaScriptModule<RCTDeviceEventEmitter>()
                .emit(eventName, parameters);
        }

        #endregion
    }
}
