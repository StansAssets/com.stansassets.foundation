using System;

namespace StansAssets.Foundation.Async
{
    interface IMainThreadDispatcher
    {
        void Init();
        void Enqueue(Action action);
    }
}
