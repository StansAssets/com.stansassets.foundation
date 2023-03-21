using UnityEngine;

namespace StansAssets.Foundation.Editor.Extensions
{
    /// <summary>
    /// <see cref="UnityEngine.Rect"/> extension methods.
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Sets <see cref="UnityEngine.Rect"/> width.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="width">New width of the <see cref="UnityEngine.Rect"/>.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with the given <paramref name="width"/>.</returns>
        public static Rect WithWidth(this Rect @this, float width)
        {
            return new Rect(@this.x, @this.y, width, @this.height);
        }
        
        /// <summary>
        /// Sets <see cref="UnityEngine.Rect"/> height.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="height">New height of the <see cref="UnityEngine.Rect"/>.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with the given <paramref name="height"/>.</returns>
        public static Rect WithHeight(this Rect @this, float height)
        {
            return new Rect(@this.x, @this.y, @this.width, height);
        }
        
        /// <summary>
        /// Sets <see cref="UnityEngine.Rect"/> size.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="size">New size of the <see cref="UnityEngine.Rect"/>.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with the given <paramref name="size"/>.</returns>
        public static Rect WithSize(this Rect @this, Vector2 size)
        {
            return new Rect(@this.x, @this.y, size.x, size.y);
        }

        /// <summary>
        /// Shifts <see cref="UnityEngine.Rect"/> horizontally (alongside X axis).
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="offset">Horizontal offset.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with the given horizontal <paramref name="offset"/>.</returns>
        public static Rect ShiftHorizontally(this Rect @this, float offset)
        {
            return new Rect(@this.x + offset, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Shifts <see cref="UnityEngine.Rect"/> vertically (alongside Y axis).
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="offset">Vertical offset.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with the given vertical <paramref name="offset"/>.</returns>
        public static Rect ShiftVertically(this Rect @this, float offset)
        {
            return new Rect(@this.x, @this.y + offset, @this.width, @this.height);
        }

        /// <summary>
        /// Extends <see cref="UnityEngine.Rect"/> width.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="width">Additional width.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with extended width.</returns>
        public static Rect AddWidth(this Rect @this, float width)
        {
            return new Rect(@this.x, @this.y, @this.width + width, @this.height);
        }

        /// <summary>
        /// Extends <see cref="UnityEngine.Rect"/> height.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="height">Additional height.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with extended height.</returns>
        public static Rect AddHeight(this Rect @this, float height)
        {
            return new Rect(@this.x, @this.y, @this.width, @this.height + height);
        }

        /// <summary>
        /// Extends <see cref="UnityEngine.Rect"/> size.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="size">Additional size.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with extended size.</returns>
        public static Rect AddSize(this Rect @this, Vector2 size)
        {
            return new Rect(@this.x, @this.y, @this.width + size.x, @this.height + size.y);
        }

        /// <summary>
        /// Moves the <see cref="UnityEngine.Rect"/> by <paramref name="x"/> along the X axis, <paramref name="y"/> along the Y axis.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="x">X-axis movement distance.</param>
        /// <param name="y">Y-axis movement distance.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with applied translation.</returns>
        public static Rect Translate(this Rect @this, float x, float y)
        {
            return new Rect(@this.x + x, @this.y + y, @this.width, @this.height);
        }

        /// <summary>
        /// Applies padding to the <see cref="UnityEngine.Rect"/>.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="left">Left-side padding.</param>
        /// <param name="top">Top-side padding.</param>
        /// <param name="right">Right-side padding.</param>
        /// <param name="bottom">Bottom-side padding.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with applied padding.</returns>
        public static Rect Pad(this Rect @this, float left, float top, float right, float bottom)
        {
            return new Rect(@this.x + left, @this.y + top, 
                @this.width - left - right, 
                @this.height - top - bottom);
        }

        /// <summary>
        /// Applies unified padding from each side to the <see cref="UnityEngine.Rect"/>.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="padding">Unified padding value (will be applied to left, top, right, bottom sides accordingly).</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with applied padding.</returns>
        public static Rect PadSides(this Rect @this, float padding)
        {
            return new Rect(@this.x + padding, @this.y + padding, 
                @this.width - padding * 2f, 
                @this.height - padding * 2f);
        }

        /// <summary>
        /// Positions the <see cref="UnityEngine.Rect"/> horizontally on the right side of the <paramref name="other"/>.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="other"><see cref="UnityEngine.Rect"/> which will be used as a relative for positioning.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with updated position.</returns>
        public static Rect RightOf(this Rect @this, Rect other)
        {
            return new Rect(other.x + other.width, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Positions the <see cref="UnityEngine.Rect"/> horizontally on the left side of the <paramref name="other"/>.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="other"><see cref="UnityEngine.Rect"/> which will be used as a relative for positioning.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with updated position.</returns>
        public static Rect LeftOf(this Rect @this, Rect other)
        {
            return new Rect(other.x - @this.width, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Positions the <see cref="UnityEngine.Rect"/> vertically above the <paramref name="other"/>.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="other"><see cref="UnityEngine.Rect"/> which will be used as a relative for positioning.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with updated position.</returns>
        public static Rect Above(this Rect @this, Rect other)
        {
            return new Rect(@this.x, other.y - @this.height, @this.width, @this.height);
        }
        
        /// <summary>
        /// Positions the <see cref="UnityEngine.Rect"/> vertically below the <paramref name="other"/>.
        /// </summary>
        /// <param name="this">The source <see cref="UnityEngine.Rect"/>.</param>
        /// <param name="other"><see cref="UnityEngine.Rect"/> which will be used as a relative for positioning.</param>
        /// <returns>New <see cref="UnityEngine.Rect"/> instance with updated position.</returns>
        public static Rect Below(this Rect @this, Rect other)
        {
            return new Rect(@this.x, other.y + other.height, @this.width, @this.height);
        }
    }
}
