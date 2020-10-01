using System;
using System.Collections.Concurrent;

namespace StansAssets.Foundation.Patterns
{
    /// <inheritdoc cref="IServiceLocator"/>
    /// <remarks>
    /// An implementation of the <see cref="IServiceLocator"/> pattern.
    /// </remarks>
    public sealed class ServiceLocator : IServiceLocator
    {
        readonly ConcurrentDictionary<Type, object> m_Services = new ConcurrentDictionary<Type, object>();

        /// <inheritdoc cref="IServiceLocator.Get" />
        public T Get<T>() => (T)Get(typeof(T));

        /// <inheritdoc cref="IServiceLocator.Get{T}" />
        public object Get(Type type)
        {
            if (!m_Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service was never registered for {type.FullName} type.");
            }

            return m_Services[type];
        }

        /// <inheritdoc cref="IServiceLocator.IsRegistered{T}" />
        public bool IsRegistered<T>() => IsRegistered(typeof(T));

        /// <inheritdoc cref="IServiceLocator.IsRegistered" />
        public bool IsRegistered(Type type)
        {
            return m_Services.ContainsKey(type);
        }

        /// <inheritdoc cref="IServiceLocator.Register{T}" />
        public void Register<T>(T service) => Register(typeof(T), service);

        /// <inheritdoc cref="IServiceLocator.Register" />
        public void Register(Type type, object service)
        {
            if (m_Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service is already registered for {type.FullName} type.");
            }

            m_Services.TryAdd(type, service);
        }

        /// <inheritdoc cref="IServiceLocator.Unregister{T}" />
        public void Unregister<T>() => Unregister(typeof(T));

        /// <inheritdoc cref="IServiceLocator.Unregister" />
        public void Unregister(Type type)
        {
            if (!m_Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service was never registered for {type.FullName} type.");
            }

            m_Services.TryRemove(type, out var _);
        }

        /// <inheritdoc cref="IServiceLocator.Clear" />
        public void Clear() => m_Services.Clear();
    }
}
