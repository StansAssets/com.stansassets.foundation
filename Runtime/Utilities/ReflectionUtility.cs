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
        /// Creates an instance of the specified <see cref="System.Type"/> using that type's parameterless constructor.
        /// </summary>
        /// <param name="typeFullName">Full type name of the instance to create.</param>
        /// <returns>New <see cref="System.Object"/> instance of the specified type.</returns>
        public static object CreateInstance(string typeFullName)
        {
            var type = FindType(typeFullName);
            return type != null && type.HasDefaultConstructor()
                ? Activator.CreateInstance(type)
                : null;
        }

        /// <summary>
        /// Searches for the specified <see cref="System.Type"/> in all assemblies of the current application domain.
        /// </summary>
        /// <param name="typeFullName">Full type's name to search for.</param>
        /// <returns><see cref="System.Type"/> object found via specified <paramref name="typeFullName"/>.</returns>
        public static Type FindType(string typeFullName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.FullName == null || type.FullName.Equals(typeFullName));
        }

        /// <summary>
        /// Searched for the implementations of the <see cref="System.Type"/> specified with <typeparamref name="T"/>.
        /// </summary>
        /// <param name="ignoreBuiltIn"><c>true</c> if the built-in assemblies have to be skipped. If set to <c>false</c>, no assemblies will be skipped.</param>
        /// <typeparam name="T">Specifies the <see cref="System.Type"/> whose implementations to search for.</typeparam>
        /// <returns>A collection of <see cref="System.Type"/> objects that are implementations of <typeparamref name="T"/>.</returns>
        public static IEnumerable<Type> FindImplementationsOf<T>(bool ignoreBuiltIn = false)
        {
            var baseType = typeof(T);
            return FindImplementationsOf(baseType, ignoreBuiltIn);
        }

        /// <summary>
        /// Gets the assemblies that have been loaded into the execution context of this application domain.
        /// </summary>
        /// <param name="ignoreBuiltIn"><c>true</c> if the built-in assemblies have to be skipped. If set to <c>false</c>, no assemblies will be skipped.</param>
        /// <returns>A collection of assemblies in this application domain.</returns>
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
        /// Searched for the implementations of the <see cref="System.Type"/> specified with <paramref name="baseType"/>.
        /// </summary>
        /// <param name="baseType">Specifies the <see cref="System.Type"/> whose implementations to search for.</param>
        /// <param name="ignoreBuiltIn"><c>true</c> if the built-in assemblies have to be skipped. If set to <c>false</c>, no assemblies will be skipped.</param>
        /// <returns>A collection of <see cref="System.Type"/> objects that are implementations of <paramref name="baseType"/>.</returns>
        public static IEnumerable<Type> FindImplementationsOf(Type baseType, bool ignoreBuiltIn = false)
        {
            var assemblies = GetAssemblies(ignoreBuiltIn);

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType != type && baseType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);
        }

        /// <summary>
        /// Returns the property value of a specified object.
        /// </summary>
        /// <param name="src">The object whose property value will be returned.</param>
        /// <param name="propName">The string containing the name of the public property to get.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <returns>The property value of the specified object.</returns>
        public static object GetPropertyValue(object src, string propName, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public)
        {
            return src.GetType().GetProperty(propName, bindingAttr)?.GetValue(src, null);
        }

        /// <summary>
        /// Sets the property value of a specified object.
        /// </summary>
        /// <param name="src">The object whose property value will be set.</param>
        /// <param name="propName">The string containing the name of the property to get.</param>
        /// <param name="propValue">The new property value.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <typeparam name="T">Specifies the <see cref="System.Type"/> of property value to set.</typeparam>
        public static void SetPropertyValue<T>(object src, string propName, T propValue, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public)
        {
            src.GetType().GetProperty(propName, bindingAttr)?.SetValue(src, propValue);
        }

        /// <summary>
        /// Searches for the methods with custom attribute of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="methodBindingFlags">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <param name="inherit"><c>true</c> to search this member's inheritance chain to find the attributes; otherwise, <c>false</c>.</param>
        /// <param name="ignoreBuiltIn"><c>true</c> if the built-in assemblies have to be skipped. If set to <c>false</c>, no assemblies will be skipped.</param>
        /// <typeparam name="T">Specifies the <see cref="System.Type"/> of the custom attribute to search for.</typeparam>
        /// <returns>A collection of <see cref="System.Reflection.MethodInfo"/> objects representing all methods defined for the current <see cref="System.Type"/> that match the specified binding constraints and attributes type.</returns>
        public static IEnumerable<MethodInfo> FindMethodsWithCustomAttributes<T>(BindingFlags methodBindingFlags = BindingFlags.Instance | BindingFlags.Public, bool inherit = true, bool ignoreBuiltIn = false) where T : Attribute
        {
            var assemblies = GetAssemblies(ignoreBuiltIn);

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods(methodBindingFlags))
                .Where(methodInfo => methodInfo.GetCustomAttributes(typeof(T), inherit).Length > 0);
        }
    }
}
