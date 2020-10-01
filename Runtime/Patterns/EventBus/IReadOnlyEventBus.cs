using System;

namespace StansAssets.Foundation.Patterns
{
    public interface IReadOnlyEventBus
    {
        void Subscribe<T>(Action<T> listener) where T : IEvent;
        void UnSubscribe<T>(Action<T> listener) where T : IEvent;
    }
}
