using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReactNative.Bridge
{
    /// <summary>
    /// Utility for managing the application dispatcher
    /// </summary>
    public static class DispatcherHelpers
    {
        private static Dispatcher s_mainDispatcher;

        /// <summary>
        /// Gets the main dispatcher
        /// </summary>
        public static Dispatcher MainDispatcher
        {
            get
            {
                AssertDispatcherSet();

                return s_mainDispatcher;
            }

            internal set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (value.Thread.GetApartmentState() != ApartmentState.STA)
                {
                    throw new ArgumentException("Dispatcher must be an STA thread");
                }

                s_mainDispatcher = value;
            }
        }

        /// <summary>
        /// Sets the main dispatcher.
        /// </summary>
        public static void Initialize()
        {
            s_mainDispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// Gets a value indicating whetehr the dispatcher has been set
        /// </summary>
        /// <returns></returns>
        public static bool IsDispatcherSet()
        {
            return s_mainDispatcher != null;
        }

        /// <summary>
        /// Assert that the current thread is running on the dispatcher
        /// </summary>
        public static void AssertOnDispatcher()
        {
            if (!IsOnDispatcher())
            {
                throw new InvalidOperationException("Thread does not have dispatcher access.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current thread is the dispatcher thread
        /// </summary>
        /// <returns></returns>
        public static bool IsOnDispatcher()
        {
            AssertDispatcherSet();

            return MainDispatcher.CheckAccess();
        }

        /// <summary>
        /// Invoke the action on the dispatcher thread
        /// </summary>
        /// <param name="action">The action to invoke</param>
        public static async void RunOnDispatcher(Action action)
        {
            AssertDispatcherSet();

            await MainDispatcher.InvokeAsync(action).Task.ConfigureAwait(false);
        }

        /// <summary>
        /// Invoke the action on the dispatcher thread with the given priority
        /// </summary>
        /// <param name="priority">The dispatcher priority</param>
        /// <param name="action">The action to invoke</param>
        public static async void RunOnDispatcher(DispatcherPriority priority, Action action)
        {
            AssertDispatcherSet();

            await MainDispatcher.InvokeAsync(action, priority).Task.ConfigureAwait(false);
        }

        /// <summary>
        /// Invoke the function on the dispatcher
        /// </summary>
        /// <typeparam name="T">The return type of the result</typeparam>
        /// <param name="func">The function to invoke</param>
        /// <returns></returns>
        public static Task<T> CallOnDispatcher<T>(Func<T> func)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();

            RunOnDispatcher(() =>
            {
                var result = func();

                // TaskCompletionSource<T>.SetResult can call continuations
                // on the awaiter of the task completion source.
                Task.Run(() => taskCompletionSource.SetResult(result));
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Reset
        /// </summary>
        public static void Reset()
        {
            // No-op on WPF
        }

        private static void AssertDispatcherSet()
        {
            if (s_mainDispatcher == null)
            {
                throw new InvalidOperationException("Dispatcher has not been set");
            }
        }
    }
}
