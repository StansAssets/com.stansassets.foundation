using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace StansAssets.Foundation.Async
{
    abstract class MainThreadDispatcherBase : IMainThreadDispatcher
    {
        static readonly ConcurrentQueue<Action> s_ExecutionQueue = new ConcurrentQueue<Action>();
        private Stopwatch sw = new Stopwatch();
        private float maxMillisecondsExecution = 100000;


        public abstract void Init();

        public void Enqueue(Action action) => s_ExecutionQueue.Enqueue(action);

        protected void Update()
        {
            while (s_ExecutionQueue.TryDequeue(out var action) && sw.ElapsedMilliseconds < maxMillisecondsExecution)
            {
                sw.Start();
                action.Invoke();
                sw.Restart();
                Debug.Log(sw.ElapsedMilliseconds);
            }
        }
    }
}