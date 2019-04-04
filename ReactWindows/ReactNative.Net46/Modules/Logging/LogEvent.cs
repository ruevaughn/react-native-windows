using ReactNative.Bridge;
using ReactNative.Modules.Core;

namespace ReactNative.Net46.Modules.Logging
{
    internal class LogEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> class.
        /// </summary>
        /// <param name="eventType">Enumeration describing the event type</param>
        public LogEvent(string eventType)
        {
            this.EventName = eventType;
        }

        /// <summary>
        /// Gets the JavaScript-facing event name associated with event type
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// Dispatch the event to the Javascript Module
        /// </summary>
        /// <param name="context">The current react context</param>
        /// <param name="data">The data to be sent with the event</param>
        public void Dispatch(ReactContext context, object data)
        {
            context.GetJavaScriptModule<RCTDeviceEventEmitter>().emit(this.EventName, data);
        }
    }
}
