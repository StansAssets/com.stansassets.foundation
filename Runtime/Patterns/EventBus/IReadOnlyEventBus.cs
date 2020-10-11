using System;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Interface allows to subscribe and unsubscribe from event bus events.
    /// But hides and ability to dispatch an event.
    /// </summary>
    public interface IReadOnlyEventBus
    {
        /// <summary>
        /// Subscribes listener to a certain event type.
        /// </summary>
        /// <param name="listener">Listener instance.</param>
        /// <typeparam name="T">An event type to subscribe for.</typeparam>
        void Subscribe<T>(Action<T> listener) where T : IEvent;
       
        /// <summary>
        /// Unsubscribes listener to a certain event type.
        /// </summary>
        /// <param name="listener">Listener instance.</param>
        /// <typeparam name="T">An event type to unsubscribe for.</typeparam>
        void Unsubscribe<T>(Action<T> listener) where T : IEvent;
    }
}
