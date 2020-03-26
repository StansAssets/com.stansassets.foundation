using System;
using System.Linq;
using UnityEngine;

namespace StansAssets.Foundation.Editor
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

            foreach (var assembly in assemblies)
            {
//                Debug.Log(assembly.FullName);
            }

            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.FullName == null || type.FullName.Equals(typeFullName));
        }
    }
}
