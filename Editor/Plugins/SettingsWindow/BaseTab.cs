using UnityEditor;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.Editor
{
    public abstract class BaseTab : VisualElement
    {
        protected BaseTab(string uxmlPath)
        {
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            Add(visualTree.CloneTree());
        }
    }
}
