using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNative.Tracing
{
    /// <summary>
    /// Write to log mediator
    /// </summary>
    public class TraceWriter
    {
        private readonly Action<int, string, string> WriteAction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="writeAction">write to log action</param>
        public TraceWriter(Action<int, string, string> writeAction)
        {
            this.WriteAction = writeAction;
        }

        /// <summary>
        /// Write to log
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        /// <param name="exception"></param>
        public void TraceWrite(int tag, string eventName, string data, Exception exception = null)
        {
            this.WriteAction?.Invoke(tag, eventName, data);
        }
    }
}
