using UnityEditor.PackageManager;

namespace StansAssets.Foundation.Editor
{
    public static class FoundationPackage
    {
        public const string Name = "com.stansassets.foundation";
        public static readonly string RootPath = PackageManagerUtility.GetPackageRootPath(Name);
        public static readonly PackageInfo Info = PackageManagerUtility.GetPackageInfo(Name);

        public static readonly string SettingsWindowPath = $"{RootPath}/Editor/Plugins/SettingsWindow";
    }
}
