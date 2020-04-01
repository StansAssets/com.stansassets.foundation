using UnityEditor.PackageManager;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Foundation Package Static info.
    /// </summary>
    public static class FoundationPackage
    {
        /// <summary>
        /// Foundation package full name.
        /// </summary>
        public const string Name = "com.stansassets.foundation";

        /// <summary>
        /// Foundation package root path.
        /// </summary>
        public static readonly string RootPath = PackageManagerUtility.GetPackageRootPath(Name);

        /// <summary>
        /// Foundation package info.
        /// </summary>
        public static PackageInfo GetPackageInfo() => PackageManagerUtility.GetPackageInfo(Name);
    }
}
