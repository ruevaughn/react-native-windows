using System;
using SYS = System.Threading.Tasks;

namespace ReactNative.Net46.Net45
{
    /// <summary>
    /// Class replaces methods introduced in dotNet 4.6 with equvivalent for 4.5.1
    /// </summary>
    internal static class Task
    {
        /// <summary>
        /// Gets equivalent got <see cref="SYS.Task"/> CompletedTask 
        /// </summary>
        public static SYS.Task CompletedTask => SYS.Task.FromResult(0);

        /// <summary>
        /// Replaces <see cref="SYS.Task{TResult}"/> FromException(ex) method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"><see cref="Exception"/>Exception that should be returned</param>
        /// <returns></returns>
        public static SYS.Task<T> FromException<T>(Exception e)
        {
            var t = new SYS.Task<T>(() => throw e);
            t.RunSynchronously();
            return t;
        }
    }
}
