using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ReactNative.Chakra
{
    public sealed partial class JavaScriptScriptException : JavaScriptException
    {
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentException(nameof(info));
            }
            info.AddValue("Error", Error);
            base.GetObjectData(info, context);
        }
    }
}
