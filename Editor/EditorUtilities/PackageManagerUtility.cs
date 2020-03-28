using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Package Manager Utility methods.
    /// </summary>
    public static class PackageManagerUtility
    {
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

        /// <summary>
        /// Returns package root path based on package name.
        /// </summary>
        /// <param name="packageName">Package name.</param>
        /// <returns>Package root path.</returns>
        public static string GetPackageRootPath(string packageName) => "Packages/" + packageName;
    }
}
