#if UNITY_2019_4_OR_NEWER
using JetBrains.Annotations;
using StansAssets.Foundation.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    /// <summary>
    /// The LoadingSpinner control. let's you place buttons group with the labels or images
    /// </summary>
    public sealed class LoadingSpinner : VisualElement
    {
        [UsedImplicitly]
        internal new class UxmlFactory : UxmlFactory<LoadingSpinner, UxmlTraits> { }

        internal new class UxmlTraits : BindableElement.UxmlTraits { }

        bool m_IsActive;
        int m_RotationAngle;

        readonly IVisualElementScheduledItem m_ScheduledUpdate;

        const long k_RotationUpdateInterval = 1L;
        const int k_RotationAngleDelta = 10;

        /// <summary>
        /// Loading Spinner control Uss class name
        /// </summary>
        public const string UssClassName = "stansassets-loading-spinner";

        /// <summary>
        /// Creates LoadingSpinner control
        /// </summary>
        public LoadingSpinner()
        {
            AddToClassList(UssClassName);
            UIToolkitEditorUtility.ApplyStyleForInternalControl(this, nameof(LoadingSpinner));
            m_IsActive = false;

            // add child elements to set up centered spinner rotation
            var innerElement = new VisualElement();
            innerElement.AddToClassList("image");
            Add(innerElement);

            m_ScheduledUpdate = schedule.Execute(UpdateProgress).Every(k_RotationUpdateInterval);
            m_ScheduledUpdate.Pause();

            RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEventHandler, TrickleDown.TrickleDown);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanelEventHandler, TrickleDown.TrickleDown);
        }

        void OnAttachToPanelEventHandler(AttachToPanelEvent e)
        {
            Activate();
        }

        void OnDetachFromPanelEventHandler(DetachFromPanelEvent e)
        {
            Deactivate();
        }

        void UpdateProgress()
        {
            transform.rotation = Quaternion.Euler(0, 0, m_RotationAngle);
            m_RotationAngle += k_RotationAngleDelta;
            if (m_RotationAngle > 360)
                m_RotationAngle -= 360;
        }

        void Activate()
        {
            if (m_IsActive)
                return;

            m_RotationAngle = 0;
            m_ScheduledUpdate.Resume();

            m_IsActive = true;
        }

        void Deactivate()
        {
            if (!m_IsActive)
                return;

            m_ScheduledUpdate.Pause();

            m_IsActive = false;
        }
    }
}
#endif
