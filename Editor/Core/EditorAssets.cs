using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Editor assets helper methods.
    /// </summary>
    public static class EditorAssets
    {
        static readonly Dictionary<string, Texture2D> s_Icons = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Get image to use with editor UI context.
        /// Image asset import options will be changed if needed.
        /// </summary>
        /// <param name="path">project folder relative texture path.</param>
        public static Texture2D GetImage(string path)
        {
            if (s_Icons.ContainsKey(path))
                return s_Icons[path];

            var importer = (TextureImporter)AssetImporter.GetAtPath(path);

            var importRequired = false;
            if (importer.mipmapEnabled) { importer.mipmapEnabled = false; importRequired = true; }
            if (!importer.alphaIsTransparency) { importer.alphaIsTransparency = true; importRequired = true; }
            if (importer.wrapMode != TextureWrapMode.Clamp) { importer.wrapMode = TextureWrapMode.Clamp; importRequired = true; }
            if (importer.textureType != TextureImporterType.GUI) { importer.textureType = TextureImporterType.GUI; importRequired = true; }
            if (importer.npotScale != TextureImporterNPOTScale.None) { importer.npotScale = TextureImporterNPOTScale.None; importRequired = true; }
            if (importer.alphaSource != TextureImporterAlphaSource.FromInput) { importer.alphaSource = TextureImporterAlphaSource.FromInput; importRequired = true; }

            // Should we make additional option for this?
            if (importer.isReadable != true) { importer.isReadable = true; importRequired = true; }

            var settings = importer.GetPlatformTextureSettings(EditorUserBuildSettings.activeBuildTarget.ToString());
            if(settings.overridden)
            {
                settings.overridden = false;
                importer.SetPlatformTextureSettings(settings);
            }

            settings = importer.GetDefaultPlatformTextureSettings();
            if (settings.textureCompression != TextureImporterCompression.Uncompressed)
            {
                settings.textureCompression = TextureImporterCompression.Uncompressed;
                importRequired = true;
            }

            if (importRequired) {
                importer.SetPlatformTextureSettings(settings);
            }

            var tex = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
            s_Icons.Add(path, tex);

            return GetImage(path);
        }
    }
}
