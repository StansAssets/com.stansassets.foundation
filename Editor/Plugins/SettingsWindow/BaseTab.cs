#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.Editor.Plugins
{
    /// <summary>
    /// Base window tab implementation for <see cref="PackageSettingsWindow"/>
    /// </summary>

    public abstract class BaseTab : VisualElement
    {
        /// <summary>
        /// Created tab with the content of provided uxml file.
        /// </summary>
        /// <param name="uxmlPath">Project related uxml file path</param>
        protected BaseTab(string uxmlPath)
        {
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            visualTree.CloneTree(this);
            style.flexGrow = 1.0f;
        }
    }
}
#endif
