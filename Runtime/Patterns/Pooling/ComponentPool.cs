using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Component pool. Provided prefab instance will be used as a source for component entities.
    /// </summary>
    /// <typeparam name="T">The type of Component on the prefab root.</typeparam>
    public class ComponentPool<T> : ObjectPool<T> where T : Component {
        /// <summary>
        /// <see cref="ComponentPool{T}"/> default implementation will use <see cref="Object.Instantiate(sourcePrefab).GetComponent&lt;T&gt;()"/>
        /// as the create action, and <see cref="component.gameObject.SetActive"/>  when pool entity is taken or released.
        /// </summary>
        /// <param name="sourcePrefab">The prefab instance you would like to use as a source for T.</param>
        /// <param name="defaultCapacity">The default capacity the stack will be created with.</param>
        public ComponentPool(GameObject sourcePrefab, int defaultCapacity = 10)
            : base(() => Object.Instantiate(sourcePrefab).GetComponent<T>(),
                entity => entity.gameObject.SetActive(true),
                entity => entity.gameObject.SetActive(false),
                defaultCapacity: defaultCapacity)
        {

        }

        /// <summary>
        /// Component source pool default implementation will use <see cref="Object.Instantiate(Object).GetComponent&lt;T&gt;()"/>
        /// <see cref="onGet"/> and <see cref="onRelease"/> action declaration is your responsibility.
        /// </summary>
        /// <param name="sourcePrefab">The prefab instance you would like to use as a source for T.</param>
        /// <param name="onGet">Action that will be called when pool entity is created.</param>
        /// <param name="onRelease">Action that will be called when pool entity is released.</param>
        /// <param name="defaultCapacity">The default capacity the stack will be created with.</param>
        public ComponentPool(GameObject sourcePrefab, Action<T> onGet, Action<T> onRelease, int defaultCapacity = 10)
            : base(() => Object.Instantiate(sourcePrefab).GetComponent<T>(),
                onGet,
                onRelease,
                defaultCapacity: defaultCapacity)
        {

        }
    }
}
