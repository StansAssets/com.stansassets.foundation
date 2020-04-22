namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Simplified default <see cref="ObjectPool{T}"/> implementation.
    /// </summary>
    public class DefaultPool<T> : ObjectPool<T> where T : class, new()
    {
        /// <summary>
        /// Creates a new DefaultPool.
        /// </summary>
        public DefaultPool() : base(() => new T()) { }
    }
}
