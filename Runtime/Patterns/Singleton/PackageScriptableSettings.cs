using UnityEngine;

namespace StansAssets.Foundation
{
    public abstract class PackageScriptableSettings : ScriptableObject
    {
        public abstract string PackageName { get; }
        public virtual string SettingsLocations => $"Assets/Settings/{PackageName}";
    }
}
