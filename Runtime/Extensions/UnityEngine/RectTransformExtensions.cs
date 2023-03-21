using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `RectTransform` extension methods.
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Positions the left border of a <see cref="UnityEngine.RectTransform"/> by the specified amount from its anchor.
        /// </summary>
        /// <param name="rectTransform">RectTransform to operate with.</param>
        /// <param name="left">Offset value of left border from the left anchor</param>
        public static void SetLeft(this RectTransform rectTransform, float left)
        {
            rectTransform.offsetMin = new Vector2(-left, rectTransform.offsetMin.y);
        }
        
        /// <summary>
        /// Positions the right border of a <see cref="UnityEngine.RectTransform"/> by the specified amount from its anchor.
        /// </summary>
        /// <param name="rectTransform">RectTransform to operate with.</param>
        /// <param name="right">Offset value of right border from the anchor</param>
        public static void SetRight(this RectTransform rectTransform, float right)
        {
            rectTransform.offsetMax = new Vector2(right, rectTransform.offsetMax.y);
        }
        
        /// <summary>
        /// Positions the top border of a <see cref="UnityEngine.RectTransform"/> by the specified amount from its anchor.
        /// </summary>
        /// <param name="rectTransform">RectTransform to operate with.</param>
        /// <param name="top">Offset value of top border from the anchor</param>
        public static void SetTop(this RectTransform rectTransform, float top)
        {
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, top);
        }
        
        /// <summary>
        /// Positions the bottom border of a <see cref="UnityEngine.RectTransform"/> by the specified amount from its anchor.
        /// </summary>
        /// <param name="rectTransform">RectTransform to operate with.</param>
        /// <param name="bottom">Offset value of top border from the anchor</param>
        public static void SetBottom(this RectTransform rectTransform, float bottom)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -bottom);
        }
        
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
