using UnityEngine;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Base class for the plugin settings.
    /// </summary>
    public abstract class PackageScriptableSettings : ScriptableObject
    {
        /// <summary>
        /// Plugin package name.
        /// </summary>
        public abstract string PackageName { get; }

        /// <summary>
        /// Plugin settings location folder.
        /// </summary>
        public abstract string SettingsLocations { get; }
    }
}
