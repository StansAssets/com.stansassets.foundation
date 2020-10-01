using System;
using System.Collections.Generic;

namespace StansAssets.Foundation.Patterns
{
    public class EventBus : IReadOnlyEventBus
    {
        public void Subscribe<T>(Action<T> listener) where T : IEvent
        {
            EventBusDispatcher<T>.Subscribe(this, listener);
        }

        public void UnSubscribe<T>(Action<T> listener) where T : IEvent
        {
            EventBusDispatcher<T>.UnSubscribe(this, listener);
        }

        public void Dispatch<T>(T @event) where T : IEvent
        {
            EventBusDispatcher<T>.Dispatch(this, @event);
        }
    }
}
