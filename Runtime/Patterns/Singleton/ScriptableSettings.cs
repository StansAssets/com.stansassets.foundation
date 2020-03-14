using UnityEngine;

namespace  StansAssets.Foundation.Patterns
{
    public abstract class ScriptableSettings : ScriptableObject
    {
        public string VersionCode = string.Empty;

        public abstract string BasePath { get; }
        public abstract string PluginName { get; }
        public abstract string DocumentationUrl { get; }
    }
}