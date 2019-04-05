using ReactNative.Bridge;
using ReactNative.Net46.Tracing.CustomTraceListeners;

namespace ReactNative.Net46.Modules.Logging
{
    /// <summary>
    /// Handles logs event aggregator
    /// </summary>
    public class LoggingModule: ReactContextNativeModuleBase
    {
        private static ReactContext _reactContext;

        private static LogsEventAggregator logsEventAggregator;

        private static ReactNativeLogTraceListener _listener;

        /// <inheritdoc />
        public LoggingModule(ReactContext reactContext) : base(reactContext)
        {
            _reactContext = reactContext;       
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();
            logsEventAggregator = new LogsEventAggregator(_reactContext, _listener);
        }

        /// <summary>
        /// Module name
        /// </summary>
        public override string Name
        {
            get
            {
                return "LoggingModule";
            }
        }

        /// <summary>
        /// Method used to register trace listener
        /// </summary>
        /// <param name="listener">ReactNativeLogTraceListener</param>
        public static void RegisterTraceListener(ReactNativeLogTraceListener listener)
        {
            _listener = listener;
        }
    }
}
