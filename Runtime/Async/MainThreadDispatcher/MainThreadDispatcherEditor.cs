using UnityEditor;

namespace StansAssets.Foundation.Async
{
    class MainThreadDispatcherEditor : MainThreadDispatcherBase
    {
        public override void Init()
        {
#if UNITY_EDITOR
            EditorApplication.update += Update;
#else
            throw new System.NotSupportedException($"Attempted to run on  {UnityEngine.Application.platform}. Only editor platform is supported");
#endif
        }
    }
}
