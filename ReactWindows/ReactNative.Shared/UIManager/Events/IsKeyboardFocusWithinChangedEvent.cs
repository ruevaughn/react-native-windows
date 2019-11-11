using Newtonsoft.Json.Linq;

namespace ReactNative.UIManager.Events
{
    class IsKeyboardFocusWithinChangedEvent : Event
    {
        public IsKeyboardFocusWithinChangedEvent(int viewTag, bool value)
            : base(viewTag)
        {
            this.newValue = value;
        }

        public override string EventName
        {
            get
            {
                return "topIsKeyboardFocusWithinChanged";
            }
        }

        private bool newValue;

        public override void Dispatch(RCTEventEmitter eventEmitter)
        {
            var eventData = new JObject
            {
                { "target", ViewTag },
                { "value", this.newValue }
            };

            eventEmitter.receiveEvent(ViewTag, EventName, eventData);
        }
    }
}
