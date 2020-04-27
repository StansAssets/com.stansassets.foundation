using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Simplified prefab implementation.
    /// </summary>
    public class PrefabPool : ObjectPool<GameObject>
    {
        /// <summary>
        /// Prefab pool default implementation will use <see cref="Object.Instantiate(Object)"/>
        /// as the create action, and <see cref="GameObject.SetActive"/>  when object is taken or released.  />
        /// </summary>
        /// <param name="prefabLink">The prefab instance you would like to pool.</param>
        /// <param name="defaultCapacity">The default capacity the stack will be created with.</param>
        public PrefabPool(GameObject prefabLink, int defaultCapacity = 10)
            :base(
                () => Object.Instantiate(prefabLink),
                gameObject => gameObject.SetActive(true),
                gameObject => gameObject.SetActive(false),
                  defaultCapacity: defaultCapacity)
        {

        }
    }
}
