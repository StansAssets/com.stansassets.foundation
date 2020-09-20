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
