using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Vector3` extension methods.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Calculates a squared distance between current and given vectors.
        /// </summary>
        /// <param name="a">The current vector.</param>
        /// <param name="b">The given vector.</param>
        /// <returns>Returns squared distance between current and given vectors.</returns>
        public static float SqrDistance(this in Vector3 a, in Vector3 b)
        {
            var x = b.x - a.x;
            var y = b.y - a.y;
            var z = b.z - a.z;
            return ((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// Multiplies each element in Vector3 by the given scalar.
        /// </summary>
        /// <param name="a">The current vector.</param>
        /// <param name="s">The given scalar.</param>
        /// <returns>Returns new Vector3 containing the multiplied components.</returns>
        public static Vector3 MultipliedBy(this in Vector3 a, float s)
        {
            return new Vector3(
                a.x * s,
                a.y * s,
                a.z * s);
        }

        /// <summary>
        /// Multiplies each element in Vector3 a by the corresponding element of b.
        /// </summary>
        /// <param name="a">The current vector.</param>
        /// <param name="b">The given vector.</param>
        /// <returns>Returns new Vector3 containing the multiplied components of the given vectors.</returns>
        public static Vector3 MultipliedBy(this in Vector3 a, Vector3 b)
        {
            b.x *= a.x;
            b.y *= a.y;
            b.z *= a.z;

            return b;
        }

        /// <summary>
        /// Smooths a Vector3 that represents euler angles.
        /// </summary>
        /// <param name="current">The current Vector3 value.</param>
        /// <param name="target">The target Vector3 value.</param>
        /// <param name="velocity">A reference Vector3 used internally.</param>
        /// <param name="smoothTime">The time to smooth, in seconds.</param>
        /// <returns>The smoothed Vector3 value.</returns>
        public static Vector3 SmoothDampEuler(this in Vector3 current, Vector3 target, ref Vector3 velocity, float smoothTime)
        {
            Vector3 v;

            v.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, smoothTime);
            v.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, smoothTime);
            v.z = Mathf.SmoothDampAngle(current.z, target.z, ref velocity.z, smoothTime);

            return v;
        }
    }
}