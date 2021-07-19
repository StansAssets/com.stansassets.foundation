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
            if (texture is Texture2D)
            {
                return texture as Texture2D;
            }

            var nativePtr = texture.GetNativeTexturePtr();
            if (nativePtr == null) // if a target platform doesn't support Native Ptr we go blitting the texture
            {
                Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
                RenderTexture currentRT = RenderTexture.active;
                RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
                Graphics.Blit(texture, renderTexture);

                RenderTexture.active = renderTexture;
                texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                texture2D.Apply();

                RenderTexture.active = currentRT;
                RenderTexture.ReleaseTemporary(renderTexture);
                return texture2D;
            }
            
            // otherwise GetNativeTexturePtr is used
            return Texture2D.CreateExternalTexture(
                texture.width,
                texture.height,
                TextureFormat.RGB24,
                false, false,
                nativePtr);
        }
    }
}
