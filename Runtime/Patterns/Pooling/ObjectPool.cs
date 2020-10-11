using System;
using System.Collections.Generic;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Generic object pool implementation.
    /// </summary>
    /// <typeparam name="T">Type of the object pool.</typeparam>
    public class ObjectPool<T> where T : class
    {
        /// <summary>
        /// A Pooled object wraps a reference to an instance that will be returned to the pool when the Pooled object is disposed.
        /// The purpose is to automate the return of references so that they do not need to be returned manually.
        /// A PooledObject can be used like so:
        /// <code>
        /// MyClass myInstance;
        /// using(myPool.Get(out myInstance)) // When leaving the scope myInstance will be returned to the pool.
        /// {
        ///     // Do something with myInstance
        /// }
        /// </code>
        /// </summary>
        public readonly struct PooledObject : IDisposable, IEquatable<PooledObject>
        {
            readonly T m_ToReturn;
            readonly ObjectPool<T> m_Pool;

            /// <summary>
            /// Creates `IDisposable` wrapper around poolable object.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="pool"></param>
            public PooledObject(T value, ObjectPool<T> pool)
            {
                m_ToReturn = value;
                m_Pool = pool;
            }

            void IDisposable.Dispose() => m_Pool.Release(m_ToReturn);

            public bool Equals(PooledObject other)
            {
                return EqualityComparer<T>.Default.Equals(m_ToReturn, other.m_ToReturn) && Equals(m_Pool, other.m_Pool);
            }

            public override bool Equals(object obj)
            {
                return obj is PooledObject other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (EqualityComparer<T>.Default.GetHashCode(m_ToReturn) * 397) ^ (m_Pool != null ? m_Pool.GetHashCode() : 0);
                }
            }
        }

        const int k_UnsetCapacityValue = -1;

        readonly Func<T> m_ActionCreate;
        readonly Action<T> m_ActionOnGet;
        readonly Action<T> m_ActionOnRelease;
        readonly uint m_MaxSize;
        readonly IPoolStackProxy<T> m_PoolStack;
        readonly bool m_CollectionCheck;

        /// <summary>
        /// The total number of active and inactive objects.
        /// </summary>
        public int CountAll { get; private set; }

        /// <summary>
        /// Number of objects that have been created by the pool but are currently in use and have not yet been returned.
        /// </summary>
        public int CountActive => CountAll - CountInactive;

        /// <summary>
        /// Number of objects that are currently available in the pool.
        /// </summary>
        public int CountInactive => m_PoolStack.Count;

        /// <summary>
        /// Creates a new ObjectPool.
        /// </summary>
        /// <param name="actionCreate">Use to create a new instance when the pool is empty. In most cases this will just be <code>() => new T()</code></param>
        /// <param name="actionOnGet">Called when the instance is being taken from the pool.</param>
        /// <param name="actionOnRelease">Called when the instance is being returned to the pool. This could be used to clean up or disable the instance.</param>
        /// <param name="concurrent">When set to `true` enables thread-safe implementation variant. The default value is `false`. </param>
        /// <param name="collectionCheck">Collection checks are performed when an instance is returned back to the pool. An exception will be thrown if the instance is already in the pool. Collection checks are only performed in the Editor.</param>
        /// <param name="defaultCapacity">The default capacity the stack will be created with.</param>
        /// <param name="maxSize">The maximum size of the pool. When the pool reaches the max size then any further instances returned to the pool will be ignored and can be garbage collected. This can be used to prevent the pool growing to a very large size.</param>
        public ObjectPool(
            Func<T> actionCreate,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            bool concurrent = false,
            bool collectionCheck = true,
            int defaultCapacity = k_UnsetCapacityValue,
            uint maxSize = 10000)
        {

            if (maxSize == 0)
                throw new ArgumentException("Limit must be greater than 0", nameof(maxSize));

            if(defaultCapacity != k_UnsetCapacityValue && concurrent)
                throw new ArgumentException("Concurrent pool variant can't have custom default capacity.", nameof(defaultCapacity));

            if (concurrent)
            {
                m_PoolStack = new ConcurrentPoolStackProxy<T>();
            }
            else
            {
                m_PoolStack = defaultCapacity != k_UnsetCapacityValue
                    ? new PoolStackProxy<T>(defaultCapacity)
                    : new PoolStackProxy<T>();
            }

            m_ActionCreate = actionCreate ?? throw new ArgumentNullException(nameof(actionCreate));
            m_MaxSize = maxSize;
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
            m_CollectionCheck = collectionCheck;
        }

        /// <summary>
        /// Fills instance with
        /// </summary>
        /// <param name="count"></param>
        public void PreWarm(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var element = m_ActionCreate();
                CountAll++;
                Release(element);
            }
        }

        /// <summary>
        /// Get an object from the pool.
        /// </summary>
        /// <returns>A new object from the pool.</returns>
        public T Get()
        {
            if (!m_PoolStack.TryPop(out var element))
            {
                element = m_ActionCreate();
                CountAll++;
            }

            m_ActionOnGet?.Invoke(element);
            return element;
        }

        /// <summary>
        /// Try to get an object in the pool.
        /// </summary>
        /// <param name="pred">Search predicate.</param>
        /// <param name="result">An existing object from the pool or <c>null</c> if the object is not found.</param>
        /// <returns><c>true</c> if an appropriate object is found, <c>false</c> otherwise.</returns>
        public bool TryGet(Predicate<T> pred, out T result)
        {
            result = null;
            List<T> inappropriateElements = CollectionPool<List<T>, T>.Get();
            while (m_PoolStack.TryPop(out var element))
            {
                if (pred(element))
                { 
                    result = element;
                    break;
                }
                else
                {
                    inappropriateElements.Add(element);
                }
            }

            foreach (T element in inappropriateElements)
                m_PoolStack.Push(element);

            CollectionPool<List<T>, T>.Release(inappropriateElements);

            if (result != null)
            { 
                m_ActionOnGet?.Invoke(result);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get a new <see cref="PooledObject"/> which can be used to return the instance back to the pool when the PooledObject is disposed.
        /// </summary>
        /// <param name="v">Output new typed object.</param>
        /// <returns>New PooledObject</returns>
        public PooledObject Get(out T v) => new PooledObject(v = Get(), this);

        /// <summary>
        /// Release an object to the pool.
        /// </summary>
        /// <param name="element">Object to release.</param>
        public void Release(T element)
        {
            #if UNITY_EDITOR // keep heavy checks in editor
            if (m_CollectionCheck && m_PoolStack.Count > 0)
            {
                if (m_PoolStack.Contains(element))
                    throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            }
            #endif

            m_ActionOnRelease?.Invoke(element);
            if (CountInactive < m_MaxSize)
                m_PoolStack.Push(element);
        }

        /// <summary>
        /// Releases all pooled objects so they can be garbage collected.
        /// </summary>
        public void Clear()
        {
            m_PoolStack.Clear();
            CountAll = 0;
        }
    }
}