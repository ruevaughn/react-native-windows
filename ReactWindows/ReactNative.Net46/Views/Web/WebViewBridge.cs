// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using ReactNative.Views.Web.Events;

namespace ReactNative.Views.Web
{
    /// <summary>
    /// The bridge used to communicate from a <see cref="WebBrowser"/>
    /// to React Native.
    /// </summary>
    [ComVisible(true)]
    public sealed class WebViewBridge
    {
        private readonly int _tag;

        /// <summary>
        /// Instantiates the <see cref="WebViewBridge"/>.
        /// </summary>
        /// <param name="tag">The view tag.</param>
        public WebViewBridge(int tag)
        {
            _tag = tag;
        }

        /// <summary>
        /// Event fired whenever a message is posted.
        /// </summary>
        public event EventHandler<MessagePostedEventArgs> MessagePosted;

        /// <summary>
        /// Posts a message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void PostMessage(string message)
        {
            MessagePosted?.Invoke(this, new MessagePostedEventArgs(_tag, message));
        }
    }
}
