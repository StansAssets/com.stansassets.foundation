using System;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// CSharp <see cref="Type"/> extension methods.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Performs a check if <c>Type</c> can be instanced using default constructor 
        /// </summary>
        /// <param name="type">A <c>Type</c> to check</param>
        /// <returns>Returns <c>true</c> if type can be instanced and <c>false</c> otherwise</returns>
        public static bool CanCreateInstanceUsingDefaultConstructor(this Type type)
        {
            return type.IsValueType || !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
