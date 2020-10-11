using System;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Basic implementation of the <see cref="IEventBus"/>.
    /// </summary>
    public sealed class EventBus : IEventBus
    {
        /// <inheritdoc>
        ///     <cref>IEventBus.Subscribe</cref>
        /// </inheritdoc>
        public void Subscribe<T>(Action<T> listener) where T : IEvent
        {
            EventBusDispatcher<T>.Subscribe(this, listener);
        }

        /// <inheritdoc>
        ///     <cref>IEventBus.Unsubscribe</cref>
        /// </inheritdoc>
        public void Unsubscribe<T>(Action<T> listener) where T : IEvent
        {
            EventBusDispatcher<T>.Unsubscribe(this, listener);
        }

        /// <inheritdoc>
        ///     <cref>IEventBus.Post</cref>
        /// </inheritdoc>
        public void Post<T>(T @event) where T : IEvent
        {
            EventBusDispatcher<T>.Dispatch(this, @event);
        }
    }
}
