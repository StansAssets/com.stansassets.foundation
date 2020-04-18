using System;
using System.Collections.Generic;
using UnityEditor;

namespace StansAssets.Foundation.Async
{
    class MainThreadDispatcherEditor : MainThreadDispatcherBase
    {
        public override void Init()
        {
#if UNITY_EDITOR
            EditorApplication.update += Update;
#endif
        }
    }
}
