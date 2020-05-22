using System;
using UnityEngine;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    /// Unity API isn't thread safe, so in case you are using threads,
    /// and you need to call any Unity API from that thread, this class is exactly what you need.
    /// The <see cref="MainThreadDispatcher"/> is available for Editor and Play mode usage.
    /// </summary>
    public static class MainThreadDispatcher
    {
        static IMainThreadDispatcher s_MainThreadDispatcher;

        /// <summary>
        /// If you are planing to use MainThreadDispatcher,
        /// make sure you call the Init method before using any MainThreadDispatcher public API.
        ///
        /// The Init methods will initialize platform specific MainThreadDispatcher implementation.
        /// </summary>
        public static void Init()
        {
            if(s_MainThreadDispatcher != null)
                return;

            if(Application.isEditor)
                s_MainThreadDispatcher = new MainThreadDispatcherEditor();
            else
                s_MainThreadDispatcher = new MainThreadDispatcherRuntime();

            s_MainThreadDispatcher.Init();
        }

        /// <summary>
        /// Adds an <see cref="Action"/> to the main thread queue.
        /// The Action will be dispatched under a main thread on a next frame.
        /// </summary>
        /// <param name="action">The callback action.</param>
        public static void Enqueue(Action action) => s_MainThreadDispatcher.Enqueue(action);
    }
}
