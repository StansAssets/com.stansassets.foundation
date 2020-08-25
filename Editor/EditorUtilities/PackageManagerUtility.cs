using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Package Manager utility methods.
    /// </summary>
    public static class PackageManagerUtility
    {
        /// <summary>
        /// Project relative path to packages manifest.
        /// </summary>
        public const string ManifestPath = "Packages/manifest.json";

#if UNITY_2019_4_OR_NEWER
        /// <summary>
        /// Returns PackageInfo if package is installed in the project, <c>null</c> otherwise.
        /// </summary>
        /// <param name="packageName">Package name.</param>
        /// <returns>Returns PackageInfo if package is installed in the project, <c>null</c> otherwise.</returns>
        public static PackageInfo GetPackageInfo(string packageName)
        {
            return PackageInfo.FindForAssetPath(GetPackageRootPath(packageName));
        }

        /// <summary>
        /// Sync method to return all the packages installed in the project.
        /// </summary>
        /// <returns>Returns all the packages installed in the project.</returns>
        public static List<PackageInfo> GetAllProjectPackages()
        {
            return AssetDatabase.FindAssets("package")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath).ToList();
        }
#endif
        /// <summary>
        /// Returns package root path based on package name.
        /// </summary>
        /// <param name="packageName">Package name.</param>
        /// <returns>Package root path.</returns>
        public static string GetPackageRootPath(string packageName) => "Packages/" + packageName;
        /// <summary>
        /// Remove Package by name.
        /// </summary>
        /// <param name="packageName">Name of the package to remove</param>
        public static void RemovePackage(string packageName)
        {
            var manifestContent = File.ReadAllText(ManifestPath);
            var rgx = new Regex("\\s*\"" + packageName + "\" *: *\".*\"(,|(?=\\s+\\}))");
            manifestContent = rgx.Replace(manifestContent, "");
            File.WriteAllText(ManifestPath, manifestContent);
        }
    }
}
