using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Patterns
{
    public class PrefabPool : ObjectPool<GameObject>
    {
        public PrefabPool(GameObject prefabLink, bool concurrent = false, bool collectionCheck = true, int defaultCapacity = 10, uint maxSize = 10000)
            :base(
                () => Object.Instantiate(prefabLink),
                gameObject => gameObject.SetActive(true),
                gameObject => gameObject.SetActive(false),
                concurrent, collectionCheck, defaultCapacity, maxSize)
        {

        }
    }
}
