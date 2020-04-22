using System.Collections.Generic;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Generic collections pool implementation.
    /// </summary>
    /// <typeparam name="TCollection">Type of the collection.</typeparam>
    /// <typeparam name="TItem">Type of the collection item.</typeparam>
    public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new()
    {
        static readonly ObjectPool<TCollection> s_Pool = new ObjectPool<TCollection>(() => new TCollection(), null, l => l.Clear());

        /// <summary>
        /// Get new.
        /// </summary>
        /// <returns>A new HashSet</returns>
        public static TCollection Get() => s_Pool.Get();

        /// <summary>
        /// Get a new list PooledObject.
        /// </summary>
        /// <param name="value">Output typed HashSet.</param>
        /// <returns>A new HashSet PooledObject.</returns>
        public static ObjectPool<TCollection>.PooledObject Get(out TCollection value) => s_Pool.Get(out value);

        /// <summary>
        /// Release an object to the pool.
        /// </summary>
        /// <param name="toRelease">hashSet to release.</param>
        public static void Release(TCollection toRelease) => s_Pool.Release(toRelease);
    }

    /// <summary>
    ///  `List` pool.
    /// </summary>
    /// <typeparam name="T">Type of `List` items.</typeparam>
    public class ListPool<T> : CollectionPool<List<T>, T> {}

    /// <summary>
    /// `Dictionary` Pool.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DictionaryPool<TKey, TValue> : CollectionPool<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>> {}
}
