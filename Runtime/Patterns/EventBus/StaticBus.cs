using System;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// This is the simplest an fastest implementation for the event bus pattern.
    /// Since this is static bus DO NOT USE it when you making a package,
    /// Since it may conflict with user project.
    ///
    /// It only make sense to use it inside the project you maintain and own.
    /// </summary>
    /// <typeparam name="T">Event Type.</typeparam>
    public static class StaticBus<T> where T : IEvent
    {
        static Action<T> s_Action = delegate { };

        /// <summary>
        /// Subscribes listener to a certain event type.
        /// </summary>
        /// <param name="listener">Listener instance.</param>
        public static void Subscribe(Action<T> listener)
        {
            s_Action += listener;
        }

        /// <summary>
        /// Unsubscribes listener to a certain event type.
        /// </summary>
        /// <param name="listener">Listener instance.</param>
        public static void Unsubscribe(Action<T> listener)
        {
            s_Action -= listener;
        }

        /// <summary>
        /// Posts and event.
        /// </summary>
        /// <param name="event">An event instance to post.</param>
        public static void Post(T @event)
        {
            s_Action.Invoke(@event);
        }
    }
}
