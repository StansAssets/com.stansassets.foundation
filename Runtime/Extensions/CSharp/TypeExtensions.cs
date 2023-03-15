using System;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// CSharp <see cref="Type"/> extension methods.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Searches for a <i>public parameterless instance</i> constructor.
        /// </summary>
        /// <param name="type">A <see cref="System.Type"/> to check for default constructor.</param>
        /// <returns>Returns <b>true</b> if the <paramref name="type">type</paramref> can be instanced and <b>false</b> otherwise.</returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            return type.IsValueType || !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
