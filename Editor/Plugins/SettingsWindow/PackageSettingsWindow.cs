using System;
using StansAssets.Foundation.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Base class for Plugin Settings Window
    /// </summary>
    /// <typeparam name="TWindow"></typeparam>
    public abstract class PackageSettingsWindow<TWindow> : EditorWindow where TWindow : EditorWindow
    {
        protected abstract PackageInfo GetPackageInfo();
        protected abstract void OnWindowEnable(VisualElement root);
        ButtonStrip m_TabsButtons;

        public void OnEnable()
        {
            var root = rootVisualElement;

            // Import UXML
            var uxmlPath = $"{FoundationPackage.SettingsWindowPath}/PackageSettingsWindow.uxml";
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var template = visualTree.CloneTree();
            root.Add(template[0]);

            var packageInfo = GetPackageInfo();
            root.Q<Label>("displayName").text = packageInfo.displayName;
            root.Q<Label>("description").text = packageInfo.description;
            root.Q<Label>("version").text = $"Version: {packageInfo.version}";

            m_TabsButtons = root.Q<ButtonStrip>();
            m_TabsButtons.OnButtonClick += e =>
            {

            };

            OnWindowEnable(root);
        }

        protected void AddTab(string label, VisualElement content)
        {

        }

        /// <summary>
        /// Method will show and doc window next to the Inspector Window.
        /// </summary>
        /// <param name="windowTitle">Window Title.</param>
        /// <param name="icon">Window Icon.</param>
        /// <returns>
        /// Returns the first EditorWindow which is currently on the screen.
        /// If there is none, creates and shows new window and returns the instance of it.
        /// </returns>
        public static TWindow ShowTowardsInspector(string windowTitle, Texture icon)
        {
            var inspectorType = Type.GetType("UnityEditor.InspectorWindow, UnityEditor.dll");
            var window = GetWindow<TWindow>(inspectorType);
            window.Show();

            window.titleContent = new GUIContent(windowTitle, icon);
            window.minSize = new Vector2(350, 100);

            return window;
        }
    }
}