using System;
using System.Collections.Generic;
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
        protected ButtonStrip m_TabsButtons;

        protected VisualElement m_WindowRoot;
        protected readonly Dictionary<string, VisualElement> m_Tabs = new Dictionary<string, VisualElement>();

        public void OnEnable()
        {
            var root = rootVisualElement;

            // Import UXML
            var uxmlPath = $"{FoundationPackage.SettingsWindowPath}/PackageSettingsWindow.uxml";
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var template = visualTree.CloneTree();
            m_WindowRoot = template[0];
            root.Add(m_WindowRoot);

            var packageInfo = GetPackageInfo();
            root.Q<Label>("displayName").text = packageInfo.displayName.TrimStart("Stans Assets - ");
            root.Q<Label>("description").text = packageInfo.description;
            root.Q<Label>("version").text = $"Version: {packageInfo.version}";

            m_TabsButtons = root.Q<ButtonStrip>();
            m_TabsButtons.CleanUp();
            m_TabsButtons.OnButtonClick += ActivateTab;

            OnWindowEnable(root);
            ActivateTab();
        }

        void ActivateTab()
        {
            foreach (var tab in m_Tabs)
                tab.Value.RemoveFromHierarchy();

            m_WindowRoot.Add(m_Tabs[m_TabsButtons.Value]);
        }

        protected void AddTab(string label, VisualElement content)
        {
            if (!m_Tabs.ContainsKey(label))
            {
                m_TabsButtons.AddChoice(label, label);
                m_Tabs.Add(label, content);
            }
            else
            {
                throw new ArgumentException($"Tab '{label}' already added", nameof(label));
            }
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