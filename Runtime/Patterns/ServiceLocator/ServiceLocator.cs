using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace StansAssets.Foundation.Patterns
{
    /// <inheritdoc cref="IServiceLocator"/>
    /// <remarks>
    /// An implementation of the <see cref="IServiceLocator"/> pattern.
    /// </remarks>
    public class ServiceLocator : IServiceLocator
    {
        readonly Dictionary<string, object> m_Services = new Dictionary<string, object>();

        /// <inheritdoc cref="IServiceLocator.Get" />
        public T Get<T>() => (T)Get(typeof(T));

        /// <inheritdoc cref="IServiceLocator.Get{T}" />
        public object Get(Type type)
        {
            var key = type.FullName;
            Assert.IsNotNull(key);
            if (!m_Services.ContainsKey(key))
            {
                throw new InvalidOperationException($"Service was never registered for {type.FullName} type.");
            }

            return m_Services[key];
        }

        /// <inheritdoc cref="IServiceLocator.IsRegistered{T}" />
        public bool IsRegistered<T>() => IsRegistered(typeof(T));

        /// <inheritdoc cref="IServiceLocator.IsRegistered" />
        public bool IsRegistered(Type type)
        {
            var key = type.FullName;
            Assert.IsNotNull(key);
            return m_Services.ContainsKey(key);
        }

        /// <inheritdoc cref="IServiceLocator.Register{T}" />
        public void Register<T>(T service) => Register(typeof(T), service);

        /// <inheritdoc cref="IServiceLocator.Register" />
        public void Register(Type type, object service)
        {
            var key = type.FullName;
            Assert.IsNotNull(key);
            if (m_Services.ContainsKey(key))
            {
                throw new InvalidOperationException($"Service is already registered for {type.FullName} type.");
            }

            m_Services.Add(key, service);
        }

        /// <inheritdoc cref="IServiceLocator.Unregister{T}" />
        public void Unregister<T>() => Unregister(typeof(T));

        /// <inheritdoc cref="IServiceLocator.Unregister" />
        public void Unregister(Type type)
        {
            var key = type.FullName;
            Assert.IsNotNull(key);
            if (!m_Services.ContainsKey(key))
            {
                throw new InvalidOperationException($"Service was never registered for {type.FullName} type.");
            }

            m_Services.Remove(key);
        }

        /// <inheritdoc cref="IServiceLocator.Clear" />
        public void Clear() => m_Services.Clear();
    }
}
