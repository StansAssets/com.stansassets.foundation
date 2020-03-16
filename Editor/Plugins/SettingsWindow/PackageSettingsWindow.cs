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
        protected string m_ActiveChoice;
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
            root.Q<Label>("displayName").text = packageInfo.displayName;
            root.Q<Label>("description").text = packageInfo.description;
            root.Q<Label>("version").text = $"Version: {packageInfo.version}";

            m_TabsButtons = root.Q<ButtonStrip>();
            m_TabsButtons.OnButtonClick += e =>
            {
                ActivateTab(m_TabsButtons.ActiveChoice);
            };

            m_TabsButtons.CleanUp();
            OnWindowEnable(root);

            m_TabsButtons.EnsureSelectedButton();
            ActivateTab(m_TabsButtons.ActiveChoice);
        }

        void ActivateTab(string choice)
        {
            if(m_ActiveChoice != null)
                m_Tabs[m_ActiveChoice].RemoveFromHierarchy();

            m_WindowRoot.Add(m_Tabs[choice]);
            m_ActiveChoice = choice;
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