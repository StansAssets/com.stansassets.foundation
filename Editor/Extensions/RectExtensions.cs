using UnityEngine;

namespace StansAssets.Foundation.Editor.Extensions
{
    /// <summary>
    /// Unity `Rect` extension methods.
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Changing 'Rect' with a given width.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="width">New width of Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect WithWidth(this Rect @this, float width)
        {
            return new Rect(@this.x, @this.y, width, @this.height);
        }
        
        /// <summary>
        /// Changing 'Rect' with a given height.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="height">New height of Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect WithHeight(this Rect @this, float height)
        {
            return new Rect(@this.x, @this.y, @this.width, height);
        }
        
        /// <summary>
        /// Changing 'Rect' with a given size.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="size">New size of Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect WithSize(this Rect @this, Vector2 size)
        {
            return new Rect(@this.x, @this.y, size.x, size.y);
        }

        /// <summary>
        /// Horizontal Rect Shift.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="offset">The value by which the Rect will be shifted.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect ShiftHorizontally(this Rect @this, float offset)
        {
            return new Rect(@this.x + offset, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Vertical Rect Shift.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="offset">The value by which the Rect will be shifted.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect ShiftVertically(this Rect @this, float offset)
        {
            return new Rect(@this.x, @this.y + offset, @this.width, @this.height);
        }

        /// <summary>
        /// Adds width to the Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="width">The width by which the Rect will be changed.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect AddWidth(this Rect @this, float width)
        {
            return new Rect(@this.x, @this.y, @this.width + width, @this.height);
        }

        /// <summary>
        /// Adds height to the Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="height">The height by which the Rect will be changed.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect AddHeight(this Rect @this, float height)
        {
            return new Rect(@this.x, @this.y, @this.width, @this.height + height);
        }

        /// <summary>
        /// Adds size to the Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="size">The size by which the Rect will be changed.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect AddSize(this Rect @this, Vector2 size)
        {
            return new Rect(@this.x, @this.y, @this.width + size.x, @this.height + size.y);
        }

        /// <summary>
        /// Changes the position of the Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="x">X-axis shift.</param>
        /// <param name="y">Y-axis shift.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect Translate(this Rect @this, float x, float y)
        {
            return new Rect(@this.x + x, @this.y + y, @this.width, @this.height);
        }

        /// <summary>
        /// Padding of the Rect by each side.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="left">Padding of the left.</param>
        /// <param name="top">Padding of the top.</param>
        /// <param name="right">Padding of the right.</param>
        /// <param name="bottom">Padding of the bottom.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect Pad(this Rect @this, float left, float top, float right, float bottom)
        {
            return new Rect(@this.x + left, @this.y + top, 
                @this.width - left - right, 
                @this.height - top - bottom);
        }

        /// <summary>
        /// Padding of the Rect on each side with the given value.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="padding">Padding value.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect PadSides(this Rect @this, float padding)
        {
            return new Rect(@this.x + padding, @this.y + padding, 
                @this.width - padding * 2f, 
                @this.height - padding * 2f);
        }

        /// <summary>
        /// Place the Rect on the right side relative to the other Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="other">Relative Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect RightOf(this Rect @this, Rect other)
        {
            return new Rect(other.x + other.width, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Place the Rect on the left side relative to the other Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="other">Relative Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect LeftOf(this Rect @this, Rect other)
        {
            return new Rect(other.x - @this.width, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Place the Rect from above relative to the other Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="other">Relative Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect Above(this Rect @this, Rect other)
        {
            return new Rect(@this.x, other.y - @this.height, @this.width, @this.height);
        }
        
        /// <summary>
        /// Place the Rect from below relative to the other Rect.
        /// </summary>
        /// <param name="this">Rect to operate with.</param>
        /// <param name="other">Relative Rect.</param>
        /// <returns>New Rect instance.</returns>
        public static Rect Below(this Rect @this, Rect other)
        {
            return new Rect(@this.x, other.y + other.height, @this.width, @this.height);
        }
    }
}
