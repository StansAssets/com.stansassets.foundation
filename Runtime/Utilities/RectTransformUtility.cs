using System;
using System.Linq;
using UnityEngine;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Rect Transform Utility methods.
    /// </summary>
    public static class RectTransformUtility
    {
        /// <summary>
        /// Method to get Rect related to ScreenSpace, from given RectTransform.
        /// This will give the real position of this Rect on screen.
        /// </summary>
        /// <param name="transform">Original RectTransform of some object</param>
        /// <returns>New Rect instance.</returns>
        public static Rect RectTransformToScreenSpace(RectTransform transform) 
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
            rect.x -= (transform.pivot.x * size.x);
            rect.y -= ((1.0f - transform.pivot.y) * size.y);
            return rect;
        }
    }
}
