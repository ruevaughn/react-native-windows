// Copyright (c) Microsoft Corporation. All rights reserved.
// Portions derived from React Native:
// Copyright (c) 2015-present, Facebook, Inc.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactNative.Bridge;
using ReactNative.UIManager;
using ReactNative.UIManager.Annotations;
using ReactNative.Views.Web.Events;
using System;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ReactNative.Views.Web
{
    /// <summary>
    /// A view manager responsible for rendering webview.
    /// </summary>
    public class ReactWebViewManager : SimpleViewManager<WebBrowser>
    {
        private const string BLANK_URL = "about:blank";

        private const int CommandGoBack = 1;
        private const int CommandGoForward = 2;
        private const int CommandReload = 3;
        private const int CommandStopLoading = 4;
        private const int CommandPostMessage = 5;
        private const int CommandInjectJavaScript = 6;

        private const string BridgeName = "__REACT_WEB_VIEW_BRIDGE";

        private readonly ViewKeyedDictionary<WebBrowser, WebViewData> _webViewData = new ViewKeyedDictionary<WebBrowser, WebViewData>();
        private readonly ReactContext _context;

        private bool isJavaScriptEnabled;

        /// <summary>
        /// Instantiates the <see cref="ReactWebViewManager"/>.
        /// </summary>
        /// <param name="context">The React context.</param>
        public ReactWebViewManager(ReactContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The name of the view manager.
        /// </summary>
        public override string Name => "RCTWebView";

        /// <summary>
        /// The commands map for the webview manager.
        /// </summary>
        public override JObject ViewCommandsMap => new JObject
        {
            { "goBack", CommandGoBack },
            { "goForward", CommandGoForward },
            { "reload", CommandReload },
            { "stopLoading", CommandStopLoading },
            { "postMessage", CommandPostMessage },
            { "injectJavaScript", CommandInjectJavaScript },
        };

        /// <summary>
        /// Sets the background color for the <see cref="WebBrowser"/>.
        /// </summary>
        /// <param name="view">The view instance.</param>
        /// <param name="color">The masked color value.</param>
        [ReactProp(ViewProps.BackgroundColor, CustomType = "Color")]
        public void SetBackgroundColor(WebBrowser view, uint? color)
        {
            // Not Implemented
        }

        /// <summary>
        /// Sets whether JavaScript is enabled or not.
        /// </summary>
        /// <param name="view">A webview instance.</param>
        /// <param name="enabled">A flag signaling whether JavaScript is enabled.</param>
        [ReactProp("javaScriptEnabled")]
        public void SetJavaScriptEnabled(WebBrowser view, bool enabled)
        {
            this.isJavaScriptEnabled = enabled;
        }

        /// <summary>
        /// Sets whether Indexed DB is enabled or not.
        /// </summary>
        /// <param name="view">A webview instance.</param>
        /// <param name="enabled">A flag signaling whether Indexed DB is enabled.</param>
        [ReactProp("indexedDbEnabled")]
        public void SetIndexedDbEnabled(WebBrowser view, bool enabled)
        {
            // Not Implementable
        }

        /// <summary>
        /// Sets the JavaScript to be injected when the page loads.
        /// </summary>
        /// <param name="view">A view instance.</param>
        /// <param name="injectedJavaScript">The JavaScript to inject.</param>
        [ReactProp("injectedJavaScript")]
        public void SetInjectedJavaScript(WebBrowser view, string injectedJavaScript)
        {
            var webViewData = GetWebViewData(view);
            webViewData.InjectedJavaScript = injectedJavaScript;
        }

        /// <summary>
        /// Toggles whether messaging is enabled for the <see cref="WebBrowser"/>.
        /// </summary>
        /// <param name="view">The view instance.</param>
        /// <param name="messagingEnabled">
        /// <code>true</code> if messaging is allowed, otherwise <code>false</code>.
        /// </param>
        [ReactProp("messagingEnabled")]
        public void SetMessagingEnabled(WebBrowser view, bool messagingEnabled)
        {
            var webViewData = GetWebViewData(view);

            if (messagingEnabled)
            {
                var bridge = new WebViewBridge(view.GetTag());
                bridge.MessagePosted += OnMessagePosted;
                webViewData.Bridge = bridge;

                view.ObjectForScripting = webViewData.Bridge;
            }
            else if (webViewData.Bridge != null)
            {
                webViewData.Bridge.MessagePosted -= OnMessagePosted;
                webViewData.Bridge = null;

                view.ObjectForScripting = null;
            }
            else
            {
                view.ObjectForScripting = null;
            }
        }

        /// <summary>
        /// Sets webview source.
        /// </summary>
        /// <param name="view">A webview instance.</param>
        /// <param name="source">A source for the webview (either static html or an uri).</param>
        [ReactProp("source")]
        public void SetSource(WebBrowser view, JObject source)
        {
            var webViewData = GetWebViewData(view);
            webViewData.Source = source;
            webViewData.SourceUpdated = true;        	
        }

        /// <inheritdoc />
        public override void ReceiveCommand(WebBrowser view, int commandId, JArray args)
        {
            switch (commandId)
            {
                case CommandGoBack:
                    if (view.CanGoBack) view.GoBack();
                    break;
                case CommandGoForward:
                    if (view.CanGoForward) view.GoForward();
                    break;
                case CommandReload:
                    view.Refresh();
                    break;
                case CommandStopLoading:
                    view.Navigate(BLANK_URL);
                    break;
                case CommandPostMessage:
                    PostMessage(view, args[0].Value<string>());
                    break;
                case CommandInjectJavaScript:
                    InvokeScript(view, args[0].Value<string>());
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported command '{commandId}' received by '{typeof(ReactWebViewManager)}'.");
            }
        }

        /// <inheritdoc />
        public override void OnDropViewInstance(ThemedReactContext reactContext, WebBrowser view)
        {
            base.OnDropViewInstance(reactContext, view);
            view.LoadCompleted -= OnNavigationCompleted;
            view.Navigating -= OnNavigationStarting;

            RemoveWebViewData(view);
        }

        /// <inheritdoc />
        protected override WebBrowser CreateViewInstance(ThemedReactContext reactContext)
        {
            var view = new WebBrowser();
            var data = new WebViewData();
            _webViewData.AddOrUpdate(view, data);
            return view;
        }

        /// <inheritdoc />
        protected override void AddEventEmitters(ThemedReactContext reactContext, WebBrowser view)
        {
            base.AddEventEmitters(reactContext, view);
            view.LoadCompleted += OnNavigationCompleted;
            view.Navigating += OnNavigationStarting;
        }

        /// <inheritdoc />
        protected override void OnAfterUpdateTransaction(WebBrowser view)
        { 
            var webViewData = GetWebViewData(view);
            if (webViewData.SourceUpdated)
            {
                NavigateToSource(view);
                webViewData.SourceUpdated = false;
            }
        }

        private void NavigateToSource(WebBrowser view)
        {
            var webViewData = GetWebViewData(view);
            var source = webViewData.Source;
            if (source != null)
            {
                var html = source.Value<string>("html");
                if (html != null)
                {
                    var baseUrl = source.Value<string>("baseUrl");
                    if (baseUrl != null)
                    {
                        view.Source = new Uri(baseUrl);
                    }

                    view.NavigateToString(html);
                    return;
                }

                var uri = source.Value<string>("uri");
                if (uri != null)
                {
                    // HTML files need to be loaded with the ms-appx-web schema.
                    // uri = uri.Replace("ms-appx:", "ms-appx-web:");

                    string previousUri = view.Source?.OriginalString;
                    if (!String.IsNullOrWhiteSpace(previousUri) && previousUri.Equals(uri))
                    {
                        return;
                    }

                    using (var request = new HttpRequestMessage())
                    {
                        var sourceUri = new Uri(uri);

                        //If the source URI has a file URL scheme, do not form the RequestUri.
                        if (!sourceUri.IsFile)
                        {
                            request.RequestUri = sourceUri;
                        }

                        var method = source.Value<string>("method");
                        var headers = (string)source.GetValue("headers", StringComparison.Ordinal);
                        var body = source.Value<Byte[]>("body");

                        view.Navigate(sourceUri, view.Name, body, headers);
                        return;
                    }
                }
            }

            view.Navigate(new Uri(BLANK_URL));
        }

        private void InvokeScriptByName(WebBrowser view, string scriptName)
        {
            if (this.isJavaScriptEnabled)
            {
                view.InvokeScript(scriptName);
            }
        }

        private void InvokeScript(WebBrowser view, string script)
        {
            if (!string.IsNullOrWhiteSpace(script))
            {
                object[] args = {script};

                InvokeScript(view, args);
            }
        }

        private void InvokeScript(WebBrowser view, object[] args)
        {
            if (view != null && this.isJavaScriptEnabled)
            {
                view.InvokeScript("eval", args);
            }
        }

        private void PostMessage(WebBrowser view, string message)
        {
            var json = new JObject
            {
                { "data", message },
            };

            var script = "(function() {" +
                         "var event;" +
                         $"var data = {json.ToString(Formatting.None)};" +
                         "try {" +
                         "event = new MessageEvent('message', data);" +
                         "} catch (e) {" +
                         "event = document.createEvent('MessageEvent');" +
                         "event.initMessageEvent('message', true, true, data.data, data.origin, data.lastEventId, data.source);" +
                         "}" +
                         "document.dispatchEvent(event);" +
                         "})();";

            InvokeScript(view, script);
        }

        private void OnNavigationStarting(object sender, NavigatingCancelEventArgs e)
        {
            var webView = (WebBrowser)sender;
            var tag = webView.GetTag();
            webView.GetReactContext().GetNativeModule<UIManagerModule>()
                .EventDispatcher
                .DispatchEvent(
                    new WebViewLoadEvent(
                         tag,
                         WebViewLoadEvent.TopLoadingStart,
                         e.Uri?.OriginalString,
                         true,
                         webView.GetDocumentTitle("Title Unavailable"),
                         webView.CanGoBack,
                         webView.CanGoForward));

            var bridge = GetWebViewData(webView).Bridge;
            if (bridge != null)
            {
                webView.ObjectForScripting = bridge;
            }
        }

        private void OnNavigationFailed(WebBrowser webView, string status, string message)
        {
            webView.GetReactContext()
                .GetNativeModule<UIManagerModule>()
                .EventDispatcher
                .DispatchEvent(
                    new WebViewLoadingErrorEvent(
                        webView.GetTag(),
                        status,
                        message));
        }

        private void OnNavigationCompleted(object sender, NavigationEventArgs e)
        {
			var webView = (WebBrowser)sender;

            webView.GetReactContext()
            		.GetNativeModule<UIManagerModule>()
                    .EventDispatcher
                    .DispatchEvent(
                         new WebViewLoadEvent(
                            webView.GetTag(),
                            WebViewLoadEvent.TopLoadingFinish,
                            e.Uri?.OriginalString,
                            false,
                            webView.GetDocumentTitle("Title Unavailable"),
                            webView.CanGoBack,
                            webView.CanGoForward));

            if (webView.IsLoaded)
            {
                var webViewData = GetWebViewData(webView);

                var injectedJavaScript = webViewData.InjectedJavaScript;

                if (!string.IsNullOrWhiteSpace(injectedJavaScript))
                {
                    try
                    {
                        InvokeScript(webView, injectedJavaScript);
                    }
                    catch (Exception ex)
                    {
                        OnNavigationFailed(webView, "Loaded", ex.Message);
                    }
                }

                // UWP OnDOMContentLoaded does this
                //if (webViewData.Bridge != null)
                //{
                //    InvokeScript(webView, $"window.postMessage = function(data) => window.external.PostMessage(String(data))");
                //}
            }
            else
            {
                OnNavigationFailed(webView, "Unknown Error loading webview.", null);
            }
        }

        private void OnMessagePosted(object sender, MessagePostedEventArgs e)
        {
            _context.GetNativeModule<UIManagerModule>()
                .EventDispatcher
                .DispatchEvent(
                    new MessageEvent(e.Tag, e.Message));
        }

        private WebViewData GetWebViewData(WebBrowser view)
        {
            return _webViewData[view];
        }

        private void RemoveWebViewData(WebBrowser view)
        {
            var webViewData = _webViewData[view];

            if (webViewData.Bridge != null)
            {
                webViewData.Bridge.MessagePosted -= OnMessagePosted;
                webViewData.Bridge = null;
            }

            _webViewData.Remove(view);
        }

        class WebViewData
        {
            public WebViewBridge Bridge { get; set; }

            public JObject Source { get; set; }

            public bool SourceUpdated { get; set; }

            public string InjectedJavaScript { get; set; }
        }
    }
}
