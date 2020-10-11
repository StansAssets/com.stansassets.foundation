namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// An interface for the event bus pattern.
    /// </summary>
    public interface IEventBus : IReadOnlyEventBus
    {
        /// <summary>
        /// Posts and event.
        /// </summary>
        /// <param name="event">An event instance to post.</param>
        /// <typeparam name="T">Event Type.</typeparam>
        void Post<T>(T @event) where T : IEvent;
    }
}
