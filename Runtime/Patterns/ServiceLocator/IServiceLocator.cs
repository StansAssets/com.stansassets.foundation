using System;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// The Service Locator patter.
    /// This pattern gives you a simple way to decouple dependencies.
    /// There are a lot props and cons about this patters but it's up to you to decide,
    /// if this pattern fits your project.
    /// Few articles we think it worth reading before using this pattern.
    /// * <see href="https://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/">Service Locator is an Anti-Pattern</see>
    /// * <see href="https://www.derpturkey.com/service-locator-is-not-an-anti-pattern/">Service Locator is NOT an Anti-Pattern</see>
    /// </summary>
    public interface IServiceLocator : IReadOnlyServiceLocator
    {
        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        void Register<T>(T service);

        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <param name="type">Service type.</param>
        /// <param name="service">Service instance.</param>
        void Register(Type type, object service);

        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        void Unregister<T>();
        
        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <param name="type">Service type.</param>
        void Unregister(Type type);
        
        /// <summary>
        /// Unregisters all the registered services in the current container.
        /// </summary>
        void Clear();
    }
}
