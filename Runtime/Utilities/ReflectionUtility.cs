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
        /// <summary>
        /// Collection of predefined built-in assembly prefixes. <c>Mono.</c>, <c>UnityEditor.</c>, <c>Unity.</c>, <c>UnityEngine</c>, <c>System</c> and <c>mscorlib</c> prefixes included.
        /// </summary>
        public static readonly string[] BuiltInAssemblyPrefixes = { "Mono.", "UnityEditor.", "Unity.", "UnityEngine", "System", "mscorlib" };

        /// <summary>
        /// Creates an instance of the specified <see cref="System.Type"/> using that type's parameterless constructor.
        /// </summary>
        /// <param name="typeFullName">Full type name of the instance to create.</param>
        /// <returns>New <see cref="System.Object"/> instance of the specified type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="typeFullName"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><see cref="System.Type"/> specified with <paramref name="typeFullName"/> doesn't have default parameterless constructor.</exception>
        public static object CreateInstance(string typeFullName)
        {
            if (typeFullName == null)
                throw new ArgumentNullException(nameof(typeFullName));
            
            var type = FindType(typeFullName);
            if (!type.HasDefaultConstructor())
                throw new ArgumentException($"Type {typeFullName} doesn't have default parameterless constructor.");

            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Searches for the specified <see cref="System.Type"/> in all assemblies of the current application domain.
        /// </summary>
        /// <param name="typeFullName">Full type's name to search for.</param>
        /// <returns><see cref="System.Type"/> object found via specified <paramref name="typeFullName"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="typeFullName"/> parameter is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">No types matching <paramref name="typeFullName"/> found in current application domain.</exception>
        public static Type FindType(string typeFullName)
        {
            if (typeFullName == null)
                throw new ArgumentNullException(nameof(typeFullName));
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .First(type => type.FullName == null || type.FullName.Equals(typeFullName));
        }

        /// <summary>
        /// Searched for the implementations of the <see cref="System.Type"/> specified with <typeparamref name="T"/>.
        /// </summary>
        /// <param name="ignoreAssemblyPrefixes">Collection of assembly prefixes to skip. The <see cref="System.Reflection.Assembly"/> will be ignored if its name <i><b>starts with</b></i> one of these prefixes.</param>
        /// <typeparam name="T">Specifies the <see cref="System.Type"/> whose implementations to search for.</typeparam>
        /// <returns>A collection of <see cref="System.Type"/> objects that are implementations of <typeparamref name="T"/>.</returns>
        public static IEnumerable<Type> FindImplementationsOf<T>(IEnumerable<string> ignoreAssemblyPrefixes = null)
        {
            var baseType = typeof(T);
            return FindImplementationsOf(baseType, ignoreAssemblyPrefixes);
        }

        /// <summary>
        /// Gets the assemblies that have been loaded into the execution context of this application domain.
        /// </summary>
        /// <param name="ignoreAssemblyPrefixes">Collection of assembly prefixes to skip. The <see cref="System.Reflection.Assembly"/> will be ignored if its name <i><b>starts with</b></i> one of these prefixes.</param>
        /// <returns>A collection of assemblies in this application domain.</returns>
        public static IEnumerable<Assembly> GetAssemblies(IEnumerable<string> ignoreAssemblyPrefixes = null)
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (ignoreAssemblyPrefixes != null)
            {
                assemblies = assemblies.Where(assembly => {
                    var assemblyName = assembly.GetName().Name;
                    return !BuiltInAssemblyPrefixes.Any(prefix => assemblyName.StartsWith(prefix));
                });
            }

            return assemblies;
        }

        /// <summary>
        /// Searched for the implementations of the <see cref="System.Type"/> specified with <paramref name="baseType"/>.
        /// </summary>
        /// <param name="baseType">Specifies the <see cref="System.Type"/> whose implementations to search for.</param>
        /// <param name="ignoreAssemblyPrefixes">Collection of assembly prefixes to skip. The <see cref="System.Reflection.Assembly"/> will be ignored if its name <i><b>starts with</b></i> one of these prefixes.</param>
        /// <returns>A collection of <see cref="System.Type"/> objects that are implementations of <paramref name="baseType"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="baseType"/> parameter is <c>null</c>.</exception>
        public static IEnumerable<Type> FindImplementationsOf(Type baseType, IEnumerable<string> ignoreAssemblyPrefixes = null)
        {
            if (baseType == null)
                throw new ArgumentNullException(nameof(baseType));
            
            var assemblies = GetAssemblies(ignoreAssemblyPrefixes);

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
        /// <exception cref="ArgumentNullException"><paramref name="propName"/> parameter is <c>null</c>.</exception>
        /// <exception cref="TargetException">The target <paramref name="src"/> object is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Property specified with the <paramref name="propName"/> not found.</exception>
        public static object GetPropertyValue(object src, string propName, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public)
        {
            if (propName == null)
                throw new ArgumentNullException(nameof(propName));
            
            if (src == null)
                throw new TargetException($"Target {nameof(src)} object is null.");

            var property = src.GetType().GetProperty(propName, bindingAttr);
            if (property == null)
                throw new ArgumentException($"Property with '{propName}' name not found");
            
            return property.GetValue(src, null);
        }

        /// <summary>
        /// Sets the property value of a specified object.
        /// </summary>
        /// <param name="src">The object whose property value will be set.</param>
        /// <param name="propName">The string containing the name of the property to get.</param>
        /// <param name="propValue">The new property value.</param>
        /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <typeparam name="T">Specifies the <see cref="System.Type"/> of property value to set.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="propName"/> parameter is <c>null</c>.</exception>
        /// <exception cref="TargetException">The target <paramref name="src"/> object is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Property specified with the <paramref name="propName"/> not found.</exception>
        public static void SetPropertyValue<T>(object src, string propName, T propValue, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public)
        {
            if (propName == null)
                throw new ArgumentNullException(nameof(propName));

            if (src == null)
                throw new TargetException($"Target {nameof(src)} object is null.");
            
            var property = src.GetType().GetProperty(propName, bindingAttr);
            if (property == null)
                throw new ArgumentException($"Property with '{propName}' name not found");
            
            property.SetValue(src, propValue);
        }

        /// <summary>
        /// Searches for the methods with custom attribute of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="methodBindingFlags">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
        /// <param name="inherit"><c>true</c> to search this member's inheritance chain to find the attributes; otherwise, <c>false</c>.</param>
        /// <param name="ignoreAssemblyPrefixes">Collection of assembly prefixes to skip. The <see cref="System.Reflection.Assembly"/> will be ignored if its name <i><b>starts with</b></i> one of these prefixes.</param>
        /// <typeparam name="T">Specifies the <see cref="System.Type"/> of the custom attribute to search for.</typeparam>
        /// <returns>A collection of <see cref="System.Reflection.MethodInfo"/> objects representing all methods defined for the current <see cref="System.Type"/> that match the specified binding constraints and attributes type.</returns>
        public static IEnumerable<MethodInfo> FindMethodsWithCustomAttributes<T>(BindingFlags methodBindingFlags = BindingFlags.Instance | BindingFlags.Public, bool inherit = true, IEnumerable<string> ignoreAssemblyPrefixes = null) where T : Attribute
        {
            var assemblies = GetAssemblies(ignoreAssemblyPrefixes);

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods(methodBindingFlags))
                .Where(methodInfo => methodInfo.GetCustomAttributes(typeof(T), inherit).Length > 0);
        }
    }
}
