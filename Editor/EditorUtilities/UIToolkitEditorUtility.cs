#if UNITY_2019_4_OR_NEWER
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Editor related UI Toolkit utility methods.
    /// </summary>
    public static class UIToolkitEditorUtility
    {
        /// <summary>
        /// Helper method to Clone VisualTreeAsset into the target and apply uss styles including the skin.
        /// For example if you provide path `Assets/MyWindow/AwesomeTab` the following will happen:
        /// * The `Assets/MyWindow/AwesomeTab.uxml` cloned into the `target`
        /// * The `Assets/MyWindow/AwesomeTab.uss` style is applied for the `target` if file exists.
        /// * The `Assets/MyWindow/AwesomeDark.uss` or `Assets/MyWindow/AwesomeLight.uss` (depends on current editor skin option)
        /// style is applied for the `target` if file exists.
        /// </summary>
        /// <param name="target">The target to clone  visual tree and apply styles to.</param>
        /// <param name="path">The VisualTreeAsset path without extension. </param>
        public static void CloneTreeAndApplyStyle(VisualElement target, string path)
        {
            var uxmlPath = $"{path}.uxml";
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            if (visualTree == null)
            {
                Debug.LogError($"Failed to load VisualTreeAsset at path: {uxmlPath}");
                return;
            }
            
            visualTree.CloneTree(target);
            ApplyStyle(target, path);
        }

        /// <summary>
        /// Helper method to Apply Style & Skin style to the target.
        /// For example if you provide path `Assets/MyWindow/AwesomeTab` the following styles will apply:
        /// * `Assets/MyWindow/AwesomeTab.uss` if file exists.
        /// * `Assets/MyWindow/AwesomeDark.uss` or `Assets/MyWindow/AwesomeLight.uss` (depends on current editor skin option) if file exists.
        /// </summary>
        /// <param name="target">The target to apply styles to.</param>
        /// <param name="path">The StyleSheet path without extension. </param>
        public static void ApplyStyle(VisualElement target, string path)
        {
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{path}.uss");
            var ussSkinPath = EditorGUIUtility.isProSkin
                ? $"{path}Dark.uss"
                : $"{path}Light.uss";

            var skitStylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ussSkinPath);
            if(stylesheet != null)
                target.styleSheets.Add(stylesheet);

            if(skitStylesheet != null)
                target.styleSheets.Add(skitStylesheet);
        }

        internal static void ApplyStyleForInternalControl<T>(T target) where T : VisualElement
        {
            var name = typeof(T).Name;
            ApplyStyleForInternalControl(target, name);
        }
        
        internal static void ApplyStyleForInternalControl(VisualElement target, string name)
        {
            ApplyStyle(target, $"{FoundationPackage.UIToolkitControlsPath}/{name}/{name}");
        }
    }
}
#endif
