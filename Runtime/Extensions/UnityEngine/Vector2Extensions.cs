using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Vector2` extension methods.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Calculates a squared distance between current and given vectors.
        /// </summary>
        /// <param name="a">The current vector.</param>
        /// <param name="b">The given vector.</param>
        /// <returns>Returns squared distance between current and given vectors.</returns>
        public static float SqrDistance(this in Vector2 a, in Vector2 b)
        {
            var x = b.x - a.x;
            var y = b.y - a.y;
            return ((x * x) + (y * y));
        }

        /// <summary>
        /// Multiplies each element in Vector2 by the given scalar.
        /// </summary>
        /// <param name="a">The current vector.</param>
        /// <param name="s">The given scalar.</param>
        /// <returns>Returns new Vector2 containing the multiplied components.</returns>
        public static Vector2 MultipliedBy(this in Vector2 a, float s)
        {
            return new Vector2(
                a.x * s,
                a.y * s);
        }

        /// <summary>
        /// Multiplies each element in Vector2 a by the corresponding element of b.
        /// </summary>
        /// <param name="a">The current vector.</param>
        /// <param name="b">The given vector.</param>
        /// <returns>Returns new Vector2 containing the multiplied components of the given vectors.</returns>
        public static Vector2 MultipliedBy(this in Vector2 a, Vector2 b)
        {
            b.x *= a.x;
            b.y *= a.y;

            return b;
        }
    }
}