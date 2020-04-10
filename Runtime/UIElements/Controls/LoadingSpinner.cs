using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    public sealed class LoadingSpinner : VisualElement
    {
        /// <exclude/>
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<LoadingSpinner, UxmlTraits> { }

        /// <summary>
        /// <exclude/>
        /// </summary>
        public new class UxmlTraits : BindableElement.UxmlTraits { }

        public bool Started { get; private set; }

        int m_Rotation;

        /// <summary>
        /// Loading Spinner control Uss class name
        /// </summary>
        public const string UssClassName = "stansassets-loading-spinner";

        public LoadingSpinner()
        {
            styleSheets.Add( Resources.Load<StyleSheet>("LoadingSpinner"));
            AddToClassList(UssClassName);

            Started = false;
            UIUtils.SetElementDisplay(this, false);

            // add child elements to set up centered spinner rotation
            var innerElement = new VisualElement();
            innerElement.AddToClassList("image");
            Add(innerElement);
        }

        void UpdateProgress()
        {
            transform.rotation = Quaternion.Euler(0, 0, m_Rotation);
            m_Rotation += 3;
            if (m_Rotation > 360)
                m_Rotation -= 360;
        }

        public void Start()
        {
            if (Started)
                return;

            m_Rotation = 0;

            EditorApplication.update += UpdateProgress;

            Started = true;
            UIUtils.SetElementDisplay(this, true);
        }

        public void Stop()
        {
            if (!Started)
                return;

            EditorApplication.update -= UpdateProgress;

            Started = false;
            UIUtils.SetElementDisplay(this, false);
        }
    }
}
