using System;

namespace StansAssets.Foundation.Patterns
{
    public static class StaticBus<T> where T : IEvent
    {
        static Action<T> s_Action = delegate { };

        public static void Subscribe(Action<T> listener)
        {
            s_Action += listener;
        }

        public static void UnSubscribe(Action<T> listener)
        {
            s_Action -= listener;
        }

        public static void Dispatch(T @event)
        {
            s_Action.Invoke(@event);
        }
    }
}
