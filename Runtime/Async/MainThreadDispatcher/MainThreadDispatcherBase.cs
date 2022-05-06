using System;
using System.Collections.Concurrent;

namespace StansAssets.Foundation.Async
{
    abstract class MainThreadDispatcherBase : IMainThreadDispatcher
    {
        static readonly ConcurrentQueue<Action> s_ExecutionQueue = new ConcurrentQueue<Action>();

        public abstract void Init();

        public void Enqueue(Action action) => s_ExecutionQueue.Enqueue(action);
        //TODO: Implement generic update methid like generic implementation : MainThreadDispatcher<T> where T : IDispatcherUpdate
        protected virtual void Update()
        {
            if(s_ExecutionQueue.TryDequeue(out var action))
                action.Invoke();
        }
    }
}
