using JetBrains.Annotations;
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

        /// <summary>
        /// Returns true if control is visible and updating it's progress
        /// </summary>
        public bool Started { get; private set; }

        readonly IVisualElementScheduledItem m_ScheduledUpdate;
        int m_RotationAngle;

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
            styleSheets.Add( Resources.Load<StyleSheet>("LoadingSpinner"));
            AddToClassList(UssClassName);

            Started = false;
            style.display = DisplayStyle.None;

            // add child elements to set up centered spinner rotation
            var innerElement = new VisualElement();
            innerElement.AddToClassList("image");
            Add(innerElement);

            m_ScheduledUpdate = schedule.Execute(UpdateProgress).Every(k_RotationUpdateInterval);
            m_ScheduledUpdate.Pause();
        }

        void UpdateProgress()
        {
            transform.rotation = Quaternion.Euler(0, 0, m_RotationAngle);
            m_RotationAngle += k_RotationAngleDelta;
            if (m_RotationAngle > 360)
                m_RotationAngle -= 360;
        }

        /// <summary>
        /// Makes control visible and starts updating it's progress
        /// </summary>
        public void Start()
        {
            if (Started)
                return;

            m_RotationAngle = 0;
            m_ScheduledUpdate.Resume();

            Started = true;
            style.display = DisplayStyle.Flex;
        }

        /// <summary>
        /// Hides control and stops updating it's progress
        /// </summary>
        public void Stop()
        {
            if (!Started)
                return;

            m_ScheduledUpdate.Pause();

            Started = false;
            style.display = DisplayStyle.None;
        }
    }
}
