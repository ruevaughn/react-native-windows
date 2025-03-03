// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace ReactNative.Views.Web.Events
{
    /// <summary>
    /// Arguments for MessagePosted event.
    /// </summary>
    public sealed class MessagePostedEventArgs
    {
        internal MessagePostedEventArgs(int tag, string message)
        {
            Tag = tag;
            Message = message;
        }

        /// <summary>
        /// The tag of the React WebView instance.
        /// </summary>
        public int Tag { get; }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; }
    }
}
