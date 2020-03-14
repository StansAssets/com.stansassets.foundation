using UnityEngine;

namespace StansAssets.Foundation.Patterns
{
    static class SingletonService
    {
        static Transform s_ServicesObjectTransform;
        public static Transform Parent
        {
            get
            {
                if (s_ServicesObjectTransform == null)
                {
                    s_ServicesObjectTransform = new GameObject("Singletons").transform;
                    Object.DontDestroyOnLoad(s_ServicesObjectTransform);
                }

                return s_ServicesObjectTransform;
            }
        }
    }
}