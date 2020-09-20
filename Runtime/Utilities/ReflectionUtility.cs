using System;
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
