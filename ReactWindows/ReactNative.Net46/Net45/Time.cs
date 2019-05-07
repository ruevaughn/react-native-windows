using System;

namespace ReactNative.Net46.Net45
{
    /// <summary>
    /// Class replaces methods introduced in dotNet 4.6 with equvivalent for 4.5.1
    /// </summary>
    internal static class Time
    {
        static long UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Millisecond;
        /// <summary>
        /// Replaces <see cref="DateTimeOffset"/>.ToUnixTimeMilliseconds method
        /// Copied from https://github.com/bluejeans/react-native-code-push/commit/b933e3388ef71b94369b7af0a7599a8a9c87eff8
        /// </summary>
        /// <param name="time">time in milliseconds</param>
        /// <returns>converted to Unix format</returns>
        public static long ToUnixTimeMilliseconds(DateTimeOffset time)
        {
            return time.ToUniversalTime().Millisecond - UnixEpoch;
        }
    }
}
