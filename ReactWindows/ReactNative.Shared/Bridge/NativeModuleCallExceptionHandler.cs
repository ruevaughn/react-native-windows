using System;

namespace ReactNative.Bridge
{
    /// <summary>
    ///     Handler for native module exceptions
    /// </summary>
    /// <remarks>
    ///     This function is called when an exception in native module occurs.
    ///     Returning "true" means that application recovered from Exception. "false" crashes the app.
    /// </remarks>
    /// <param name="ex">Exception to process</param>
    /// <returns>Was exception handled or not</returns>
    public delegate bool NativeModuleCallExceptionHandler(Exception ex);
}
