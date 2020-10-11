using System.Reflection;
using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `GameObject` extension methods.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Set layer to all GameObject children, including inactive.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="layerNumber">Layer number.</param>
        public static void SetLayerRecursively(this GameObject gameObject, int layerNumber)
        {
            foreach (var trans in gameObject.GetComponentsInChildren<Transform>(true)) 
                trans.gameObject.layer = layerNumber;
        }
        
        /// <summary>
        /// Gets the local Identifier In File, for the given GameObject
        /// Return 0 in case Game Object wasn't yet saved
        /// </summary>
        /// <param name="go">GameObject you want to check</param>
        public static int GetLocalIdentifierInFile(GameObject go)
        {
#if UNITY_EDITOR
            var inspectorModeInfo = typeof(UnityEditor.SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            var serializedObject = new UnityEditor.SerializedObject(go);
            inspectorModeInfo.SetValue(serializedObject, UnityEditor.InspectorMode.Debug, null);
            var localIdProp = serializedObject.FindProperty("m_LocalIdentfierInFile"); //note the misspelling!
            return localIdProp.intValue;
#else
            return 0;
#endif
        }
        
        /// <summary>
        /// Renderer Bounds of the game object.
        /// </summary>
        /// <param name="go">GameObject you want calculate bounds for.</param>
        /// <returns>Calculated game object bounds.</returns>
        public static Bounds GetRendererBounds(this GameObject go)
        {
            return CalculateBounds(go);
        }
        
        static Bounds CalculateBounds(GameObject obj)
        {
            var hasBounds = false;
            var bounds = new Bounds(Vector3.zero, Vector3.zero);
            var childrenRenderer = obj.GetComponentsInChildren<Renderer>();


            var rnd = obj.GetComponent<Renderer>();
            if (rnd != null)
            {
                bounds = rnd.bounds;
                hasBounds = true;
            }

            foreach (var child in childrenRenderer)
                if (!hasBounds)
                {
                    bounds = child.bounds;
                    hasBounds = true;
                }
                else
                {
                    bounds.Encapsulate(child.bounds);
                }

            return bounds;
        }
    }
    
}
