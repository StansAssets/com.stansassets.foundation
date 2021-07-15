using System;
using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Texture` extension methods.
    /// </summary>
    public static class TextureExtensions
    {
        /// <summary>
        /// Convert <see cref="Texture"/> to <see cref="Texture2D"/>.
        /// </summary>
        /// <param name="texture">Texture to convert.</param>
        /// <returns>Converted texture as Texture2D</returns>
       
        public static Texture2D ToTexture2D(this Texture texture)
        {
            if (texture is Texture2D) {
                return texture as Texture2D;
            }
            return Texture2D.CreateExternalTexture(
                texture.width,
                texture.height,
                TextureFormat.RGB24,
                false, false,
                texture.GetNativeTexturePtr());
        }

    }
}
