using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Material` extension methods.
    /// </summary>
    public static class MaterialExtensions
    {
        /// <summary>
        /// Set's alpha channel for the Material `_Color` property
        /// </summary>
        /// <param name="material">Material to operate with.</param>
        /// <param name="value">Alpha channel value.</param>
        public static void SetAlpha(this Material material, float value)
        {
            if (material.HasProperty("_Color"))
            {
                var color = material.color;
                color.a = value;
                material.color = color;
            }
        }
    }
}
