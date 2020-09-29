using System;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// The read only interfaces for the <see cref="IServiceLocator"/> pattern.
    /// Should be used if you would only like to retrieve services,
    /// without giving and ability to register new ones. 
    /// </summary>
    public interface IReadOnlyServiceLocator
    {
        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        T Get<T>();
        
        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <param name="type">The type of the service to lookup.</param>
        /// <returns>The service instance.</returns>
        object Get(Type type);
        
        /// <summary>
        /// Check if service is registered for a given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>Returns `true` if service is registered and `false` otherwise.</returns>
        bool IsRegistered<T>();
        
        /// <summary>
        /// Check if service is registered for a given type.
        /// </summary>
        /// <param name="type">The type of the service to lookup.</param>
        /// <returns>Returns `true` if service is registered and `false` otherwise.</returns>
        bool IsRegistered(Type type);
    }
}
