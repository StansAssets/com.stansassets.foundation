using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity <see cref="Vector3"/> extension methods.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Calculates a squared distance between current and given vectors.
        /// </summary>
        /// <param name="a">A current vector.</param>
        /// <param name="b">A given vector.</param>
        /// <returns>Returns squared distance between current and given vectors.</returns>
        public static float SqrDistance(this in Vector3 a, in Vector3 b)
        {
            var x = b.x - a.x;
            var y = b.y - a.y;
            var z = b.z - a.z;
            return ((x * x) + (y * y) + (z * z));
        }
    }
}