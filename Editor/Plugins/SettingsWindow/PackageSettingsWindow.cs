using System;
using System.Collections.Generic;
using StansAssets.Foundation.Extensions;
using StansAssets.Foundation.UIElements;
using UnityEditor;
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#endif
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace StansAssets.Foundation.Editor.Plugins
{
    /// <summary>
    /// Base class for Plugin Settings Window
    /// </summary>
    /// <typeparam name="TWindow"></typeparam>
    public abstract class PackageSettingsWindow<TWindow> : EditorWindow where TWindow : EditorWindow
    {
        protected abstract PackageInfo GetPackageInfo();
#if UNITY_2019_1_OR_NEWER
        protected abstract void OnWindowEnable(VisualElement root);
        protected ButtonStrip m_TabsButtons;

        protected VisualElement m_WindowRoot;
        protected readonly Dictionary<string, VisualElement> m_Tabs = new Dictionary<string, VisualElement>();

        readonly string m_WindowUIFilesRootPath = $"{FoundationPackage.RootPath}/Editor/Plugins/SettingsWindow";

        void OnEnable()
        {
            var root = rootVisualElement;

            // Import UXML
            var uxmlPath = $"{m_WindowUIFilesRootPath}/PackageSettingsWindow.uxml";
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            var ussPath = $"{m_WindowUIFilesRootPath}/PackageSettingsWindow.uss";
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ussPath);
            m_WindowRoot.styleSheets.Add(stylesheet);
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

        /// <summary>
        /// Add tab to the window top bar.
        /// </summary>
        /// <param name="label">Tab label.</param>
        /// <param name="content">Tab content.</param>
        /// <exception cref="ArgumentException">Will throw tab with the same label was already added.</exception>
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
#endif
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