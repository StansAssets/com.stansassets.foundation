using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Reflection Utility Methods.
    /// </summary>
    public static class ReflectionUtility
    {
        /// <summary>
        /// Methods will iterate all the project Assemblies.
        /// If typeFullName will match new object instance of that type will be created
        /// and returned as the result.
        /// </summary>
        /// <param name="typeFullName">Full type name.</param>
        /// <returns>New type instance.</returns>
        public static object CreateInstance(string typeFullName)
        {
            var type = FindType(typeFullName);
            return type != null ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// Methods will iterate all the project to find a type that matches sissified full type name.
        /// </summary>
        /// <param name="typeFullName">Full type name.</param>
        /// <returns>Type object.</returns>
        public static Type FindType(string typeFullName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.FullName == null || type.FullName.Equals(typeFullName));
        }

        /// <summary>
        /// Find all types that implement `T`.
        /// </summary>
        /// <typeparam name="T">Base type.</typeparam>
        /// <returns>Returns all types that are implement provided base type.</returns>
        public static IEnumerable<Type> FindImplementationsOf<T>()
        {
            var baseType = typeof(T);
            return FindImplementationsOf(baseType);
        }


        /// <summary>
        /// Find all types that implement `baseType`.
        /// </summary>
        /// <param name="baseType">Base type.</param>
        /// <returns>Returns all types that are implement provided base type.</returns>
        public static IEnumerable<Type> FindImplementationsOf(Type baseType)
        {
            var implementations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            return implementations;
        }

        /// <summary>
        /// Get property value from an object by it's name.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <param name="propName">Property name.</param>
        /// <param name="bindingAttr">Property binding Attributes. ` BindingFlags.Instance | BindingFlags.Public` by default.</param>
        /// <returns>Property value.</returns>
        public static object GetPropertyValue(object src, string propName, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public)
        {
            return src.GetType().GetProperty(propName, bindingAttr).GetValue(src, null);
        }

        /// <summary>
        /// Get property value from an object by it's name.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <param name="propName">Property name.</param>
        /// <param name="propValue">New property value.</param>
        /// <param name="bindingAttr">Property binding Attributes. ` BindingFlags.Instance | BindingFlags.Public` by default.</param>
        public static void SetPropertyValue<T>(object src, string propName, T propValue, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public)
        {
            src.GetType().GetProperty(propName, bindingAttr).SetValue(src, propValue);
        }
    }
}
