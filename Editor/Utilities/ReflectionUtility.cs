using System;

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
        /// <param name="typeFullName">full type name</param>
        /// <returns>Create type instance</returns>
        public static object CreateInstance(string typeFullName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName != null && !type.FullName.Equals(typeFullName))
                        continue;

                    return Activator.CreateInstance(type);
                }
            }
            return null;
        }
    }
}
