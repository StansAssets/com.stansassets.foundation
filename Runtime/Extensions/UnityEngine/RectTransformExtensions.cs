using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `RectTransform` extension methods.
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Resets `anchorMin`, `anchorMax`, `offsetMin`, `offsetMax` to `Vector2.zero`.
        /// </summary>
        /// <param name="rectTransform">RectTransform to operate with.</param>
        public static void Reset(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        
        /// <summary>
        /// Get's the screen rect of provided RectTransform.
        /// </summary>
        /// <param name="rectTransform">RectTransform to operate with.</param>
        /// <returns>Screen rect.</returns>
        public static Rect GetScreenRect(this RectTransform rectTransform)
        {
            var rtCorners = new Vector3[4];
            rectTransform.GetWorldCorners(rtCorners);
            var rtRect = new Rect(new Vector2(rtCorners[0].x, rtCorners[0].y), new Vector2(rtCorners[3].x - rtCorners[0].x, rtCorners[1].y - rtCorners[0].y));

            var canvas = rectTransform.GetComponentInParent<Canvas>();
            var canvasCorners = new Vector3[4];
            canvas.GetComponent<RectTransform>().GetWorldCorners(canvasCorners);
            var cRect = new Rect(new Vector2(canvasCorners[0].x, canvasCorners[0].y), new Vector2(canvasCorners[3].x - canvasCorners[0].x, canvasCorners[1].y - canvasCorners[0].y));

            var screenWidth = Screen.width;
            var screenHeight = Screen.height;

            var size = new Vector2(screenWidth / cRect.size.x * rtRect.size.x, screenHeight / cRect.size.y * rtRect.size.y);
            var rect = new Rect(screenWidth * ((rtRect.x - cRect.x) / cRect.size.x), screenHeight * ((-cRect.y + rtRect.y) / cRect.size.y), size.x, size.y);
            return rect;
        }
    }
}
