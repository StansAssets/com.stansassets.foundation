using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Object` extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs a TRUE null-check.
        /// See http://answers.unity.com/answers/1224404/view.html
        /// </summary>
        /// <param name="obj">An object to check.</param>
        /// <returns>Returns <c>true</c> if object is null, <c>false</c> otherwise.</returns>
        public static bool IsNull(this Object obj)
        {
            return (object)obj == null;
        }

        /// <summary>
        /// Performs a TRUE null-check.
        /// See http://answers.unity.com/answers/1224404/view.html
        /// </summary>
        /// <param name="obj">An object to check.</param>
        /// <returns>Returns <c>false</c> if object is null, <c>true</c> otherwise.</returns>
        public static bool IsNotNull(this Object obj)
        {
            return !IsNull(obj);
        }
    }
}