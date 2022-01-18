using System;
using System.Linq;
using UnityEngine;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Rect Transform Utility methods.
    /// </summary>
    public static class RectTransformExtensions
    //
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
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
    }
}
