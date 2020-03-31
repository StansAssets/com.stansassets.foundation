using UnityEditor.PackageManager;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Foundation Package Static info.
    /// </summary>
    public static class FoundationPackage
    {
        /// <summary>
        /// Package full name.
        /// </summary>
        public const string Name = "com.stansassets.foundation";

        /// <summary>
        /// Package root path.
        /// </summary>
        public static readonly string RootPath = PackageManagerUtility.GetPackageRootPath(Name);

        /// <summary>
        /// Package info.
        /// </summary>
        public static readonly PackageInfo Info = PackageManagerUtility.GetPackageInfo(Name);
    }
}
