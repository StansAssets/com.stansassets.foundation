using UnityEngine.SceneManagement;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Scene` extension methods.
    /// </summary>
    public static class SceneExtensions
    {
        /// <summary>
        /// Returns the component of Type `type` in the located on scene root GameObject or any of its children using depth first search.
        /// A component is returned only if it is found on an active GameObject.
        /// </summary>
        /// <param name="scene">Scene to operate with.</param>
        /// <typeparam name="T">Type of the component.</typeparam>
        /// <returns>A component of the matching type, if found.</returns>
        public static T GetComponentInChildren<T>(this Scene scene) where T : class
        {
            foreach (var gameObject in scene.GetRootGameObjects())
            {
                var component = gameObject.GetComponentInChildren<T>();
                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        /// <summary>
        /// Returns the component of Type `type` in on of the located on scene root GameObject.
        /// A component is returned only if it is found on an active GameObject.
        /// </summary>
        /// <param name="scene">Scene to operate with.</param>
        /// <typeparam name="T">Type of the component.</typeparam>
        /// <returns>A component of the matching type, if found.</returns>
        public static T GetComponent<T>(this Scene scene) where T : class
        {
            foreach (var gameObject in scene.GetRootGameObjects())
            {
                var component = gameObject.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }
    }
}