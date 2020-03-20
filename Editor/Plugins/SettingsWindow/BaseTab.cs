using UnityEditor;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.Editor
{
    public abstract class BaseTab : VisualElement
    {
        protected readonly VisualElement m_Root;

        protected BaseTab(string uxmlPath)
        {
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var template = visualTree.CloneTree();
            m_Root = template[0];

            Add(m_Root);
        }
    }
}
