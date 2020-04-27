using System;
using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Texture2D` extension methods.
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Convert <see cref="Texture2D"/> png representation to base64 string.
        /// </summary>
        /// <param name="texture">Texture to convert.</param>
        /// <returns>Converted texture as base64 string</returns>
        public static string ToBase64(this Texture2D texture)
        {
            var val = texture.EncodeToPNG();
            return Convert.ToBase64String(val);
        }

        /// <summary>
        /// Loads texture content from base64 string.
        /// </summary>
        /// <param name="texture">Texture to load image content into.</param>
        /// <param name="base64EncodedString">Base64 string image representation.</param>
        /// <returns>Updated texture.</returns>
        public static Texture2D LoadFromBase64(this Texture2D texture, string base64EncodedString)
        {
            var decodedFromBase64 = Convert.FromBase64String(base64EncodedString);
            texture.LoadImage(decodedFromBase64);
            return texture;
        }
    }
}
