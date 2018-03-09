using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ReactNative.Shared.Chakra
{
    [Serializable]
    public class JavaScriptCallFunctionException : Exception
    {
        private readonly string moduleName;
        private readonly string methodName;
        private readonly JArray arguments;

        public string ModuleName
        {
            get { return moduleName; }
        }

        public string MethodName
        {
            get { return methodName; }
        }

        public JArray Arguments
        {
            get { return arguments; }
        }

        public JavaScriptCallFunctionException(string moduleName, string methodName, JArray arguments, Exception innerException) : base("JavaScript call fuction exception", innerException)
        {
            this.moduleName = moduleName;
            this.methodName = methodName;
            this.arguments = arguments;
        }
    }
}
