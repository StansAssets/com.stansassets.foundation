using System;
using System.Collections.Generic;

namespace StansAssets.Foundation.Async
{
    abstract class MainThreadDispatcherBase : IMainThreadDispatcher
    {
        static readonly Queue<Action> s_ExecutionQueue = new Queue<Action>();

        public abstract void Init();

        public void Enqueue(Action action)
        {
            lock (s_ExecutionQueue)
                s_ExecutionQueue.Enqueue(action);
        }

        protected void Update()
        {
            lock (s_ExecutionQueue)
            {
                while (s_ExecutionQueue.Count > 0)
                {
                    var action = s_ExecutionQueue.Dequeue();
                    action?.Invoke();
                }
            }
        }
    }
}
