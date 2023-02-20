using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StansAssets.Foundation.Extensions;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Reflection Utility Methods.
    /// </summary>
    public static class ReflectionUtility
    {
        static readonly string[] s_BuiltInAssemblyPrefixes = { "Mono.", "Unity.", "UnityEngine", "UnityEditor", "System", "mscorlib" };

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
            return type != null && type.CanCreateInstanceUsingDefaultConstructor()
                ? Activator.CreateInstance(type)
                : null;
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
        public static IEnumerable<Type> FindImplementationsOf<T>(bool ignoreBuiltIn = false)
        {
            var baseType = typeof(T);
            return FindImplementationsOf(baseType, ignoreBuiltIn);
        }

        /// <summary>
        /// Return all project assemblies with option to filter the Unity built-in assemblies
        /// </summary>
        /// <param name="ignoreBuiltIn">Ignores Unity built-in assembles</param>
        /// <returns>Returns all assemblies that match filter criteria</returns>
        public static IEnumerable<Assembly> GetAssemblies(bool ignoreBuiltIn = false)
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (ignoreBuiltIn)
            {
                assemblies = assemblies.Where(assembly => {
                    var assemblyName = assembly.GetName().Name;
                    return !s_BuiltInAssemblyPrefixes.Any(prefix => assemblyName.StartsWith(prefix));
                });
            }

            return assemblies;
        }

        /// <summary>
        /// Find all types that implement `baseType`.
        /// </summary>
        /// <param name="baseType">Base type.</param>
        /// <param name="ignoreBuiltIn">Ignores Unity built-in assembles</param>
        /// <returns>Returns all types that are implement provided base type.</returns>
        public static IEnumerable<Type> FindImplementationsOf(Type baseType, bool ignoreBuiltIn = false)
        {
            var assemblies = GetAssemblies(ignoreBuiltIn);

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);
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
            return src.GetType().GetProperty(propName, bindingAttr)?.GetValue(src, null);
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
            src.GetType().GetProperty(propName, bindingAttr)?.SetValue(src, propValue);
        }

        /// <summary>
        /// Find all methods with custom attribute of type <c>T</c>.
        /// </summary>
        /// <param name="methodBindingFlags">Method binding Attributes. ` BindingFlags.Instance | BindingFlags.Public` by default.</param>
        /// <param name="inherit"><c>true</c> to search in the member's inheritance chain to find the attributes</param>
        /// <param name="ignoreBuiltIn"><c>true</c> to ignores the Unity built-in assemblies</param>
        /// <typeparam name="T">Type of the custom attribute to search</typeparam>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> FindMethodsWithCustomAttributes<T>(BindingFlags methodBindingFlags = BindingFlags.Instance | BindingFlags.Public, bool inherit = true, bool ignoreBuiltIn = false)
        {
            var assemblies = GetAssemblies(ignoreBuiltIn);

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods(methodBindingFlags))
                .Where(methodInfo => methodInfo.GetCustomAttributes(typeof(T), inherit).Length > 0);
        }
    }
}
