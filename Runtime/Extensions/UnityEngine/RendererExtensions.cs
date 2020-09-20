using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Renderer` extension methods.
    /// </summary>
    public static class RendererExtensions
    {
        /// <summary>
        /// Methods to check if renderer is visible from a certain point.
        /// </summary>
        /// <param name="renderer">Renderer to operate with.</param>
        /// <param name="camera">Camera instance tha will be used for calculation.</param>
        /// <returns></returns>
        public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
        
        /// <summary>
        /// Renderer Bounds of the renderer. Method also take children in consideration.
        /// </summary>
        /// <param name="renderer">Renderer to operate with.</param>
        /// <returns>Calculated renderer bounds.</returns>
        public static Bounds GetRendererBounds(this Renderer renderer)
        {
            return renderer.gameObject.GetRendererBounds();
        }
    }
}
