// Copyright (c) Microsoft Corporation. All rights reserved.
// Portions derived from React Native:
// Copyright (c) 2015-present, Facebook, Inc.
// Licensed under the MIT License.

using Newtonsoft.Json.Linq;
using ReactNative.UIManager.Events;

namespace ReactNative.Views.Web.Events
{
    internal class MessageEvent : Event
    {
        private readonly string _data;

        public MessageEvent(int viewTag, string data)
            : base(viewTag)
        {
            _data = data;
        }

        public override string EventName => "topMessage";

        public override bool CanCoalesce => false;

        public override void Dispatch(RCTEventEmitter eventEmitter)
        {
            eventEmitter.receiveEvent(ViewTag, EventName, new JObject
            {
                { "data", _data },
            });
        }
    }
}
