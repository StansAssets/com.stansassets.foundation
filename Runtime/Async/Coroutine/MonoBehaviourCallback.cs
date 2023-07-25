using System;
using StansAssets.Foundation.Patterns;

namespace StansAssets.Foundation.Async
{
    /// <summary>
    /// The MonoBehaviourCallback helper class is meant to be used when you need to have MonoBehaviour default Unity callbacks,
    /// but your model is not a MonoBehaviour and you dont want to convert it to the MonoBehaviour by design.
    ///
    /// Please note, that you will subscribe to the global MonoBehaviour singleton instance. Other parts of code may also use it.
    /// In case other callback users will throw and unhandled exception you may not received the callback you subscribed for.
    /// </summary>
    public class MonoBehaviourCallback : MonoSingleton<MonoBehaviourCallback>
    {
        event Action m_OnUpdate = delegate { };
        event Action m_OnLateUpdate = delegate { };
        event Action m_OnFixedUpdate = delegate { };
        event Action m_OnApplicationQuit = delegate { };
        event Action<bool> m_ApplicationOnPause = delegate { };
        event Action<bool> m_OnApplicationFocus = delegate { };
        
        /// <summary>
        /// Update is called every frame.
        /// Learn more: [MonoBehaviour.Update](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html)
        /// </summary>
        public static event Action OnUpdate
        {
            add => Instance.m_OnUpdate += value;
            remove => Instance.m_OnUpdate -= value;
        }

        /// <summary>
        /// LateUpdate is called after all Update functions have been called. This is useful to order script execution.
        /// For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
        /// Learn more: [MonoBehaviour.LateUpdate](https://docs.unity3d.com/ScriptReference/MonoBehaviour.LateUpdate.html)
        /// </summary>
        public static event Action OnLateUpdate
        {
            add => Instance.m_OnLateUpdate += value;
            remove => Instance.m_OnLateUpdate -= value;
        }
        
        /// <summary>
        /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
        /// Learn more: [MonoBehaviour.FixedUpdate](https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html)
        /// </summary>
        public static event Action OnFixedUpdate
        {
            add => Instance.m_OnFixedUpdate += value;
            remove => Instance.m_OnFixedUpdate -= value;
        }
        
        /// <summary>
        /// In the editor this is called when the user stops playmode.
        /// Learn more: [MonoBehaviour.OnApplicationQuit](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationQuit.html)
        /// </summary>
        public static event Action ApplicationOnQuit
        {
            add => Instance.m_OnApplicationQuit += value;
            remove => Instance.m_OnApplicationQuit -= value;
        }
        
        /// <summary>
        /// Sent to all GameObjects when the application pauses.
        /// Learn more: [MonoBehaviour.OnApplicationPause](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html)
        /// </summary>
        public static event Action<bool> ApplicationOnPause
        {
            add => Instance.m_ApplicationOnPause += value;
            remove => Instance.m_ApplicationOnPause -= value;
        }
        
        /// <summary>
        /// Sent to all GameObjects when the player gets or loses focus.
        /// Learn more: [MonoBehaviour.OnApplicationFocus](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationFocus.html)
        /// </summary>
        public static event Action<bool> ApplicationOnFocus
        {
            add => Instance.m_OnApplicationFocus += value;
            remove => Instance.m_OnApplicationFocus -= value;
        }

        void Update() => m_OnUpdate.Invoke();
        void LateUpdate() => m_OnLateUpdate.Invoke();
        void FixedUpdate() => m_OnFixedUpdate.Invoke();
        void OnApplicationPause(bool pauseStatus) => m_ApplicationOnPause.Invoke(pauseStatus);
        void OnApplicationFocus(bool hasFocus) => m_OnApplicationFocus.Invoke(hasFocus);

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            m_OnApplicationQuit.Invoke();
        }
    }
}
