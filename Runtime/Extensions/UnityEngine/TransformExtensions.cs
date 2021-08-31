using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Transform` extension methods.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Sets <see cref="Transform.lossyScale"/> value.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="lossyScale">New lossyScale value.</param>
        public static void SetLossyScale(this Transform transform, Vector3 lossyScale)
        {
            transform.localScale = Vector3.one;
            var currentLossyScale = transform.lossyScale;
            transform.localScale = new Vector3(lossyScale.x / currentLossyScale.x, lossyScale.y / currentLossyScale.y, lossyScale.z / currentLossyScale.z);
        }

        /// <summary>
        /// Reset <see cref="Transform"/> component position, scale and rotation.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="relativeTo">Space enum the Reset method relative to.
        /// Space.Self (default value) resets local space values.
        /// Space.World resets absolute world space values.
        /// Using World space may cause visual deformations, which depends on a parent's scale</param>
        public static void Reset(this Transform transform, Space relativeTo = Space.Self)
        {
            switch (relativeTo) {
                case Space.Self:
                    transform.localScale = Vector3.one;
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    break;
                case Space.World:
                    transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    transform.SetLossyScale(Vector3.one);
                    break;
                default:
                    throw new System.ArgumentException($"Parameter '{nameof(relativeTo)}' contains unknown value. '{nameof(Space.Self)}' and '{nameof(Space.World)}' are accepted", nameof(relativeTo));
            }
        }

        /// <summary>
        /// Removes all transform children.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="activeOnly">Will ignore disabled game-objects when set to <c>true</c>. </param>
        /// <returns></returns>
        public static Transform Clear(this Transform transform, bool activeOnly = false)
        {
            if (transform.childCount == 0)
                return transform;


            var children = transform.GetComponentsInChildren<Transform>();

            foreach (var child in children)
            {
                if (child == transform || child == null) continue;
                if (activeOnly && !child.gameObject.activeSelf)  continue;
                Object.DestroyImmediate(child.gameObject);
            }
            return transform;
        }

        /// <summary>
        /// Find or create child with name.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="name">Child name.</param>
        /// <returns>Child <see cref="Transform"/> component instance.</returns>
        public static Transform FindOrCreateChild(this Transform transform, string name) {
            var part = transform.Find(name);
            if (part == null) {
                part = new GameObject(name).transform;
                part.parent = transform;
                part.Reset();
            }
            return part;
        }

        /// <summary>
        /// Searches for a child with the provided name across the whole game objects hierarchy.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="childName">Child name.</param>
        /// <returns>Child <see cref="Transform"/> component instance.</returns>
        public static Transform FindChildRecursively(this Transform transform, string childName) {
            foreach (Transform child in transform) {
                if (child.name == childName) {
                    return child;
                }

                var found = FindChildRecursively(child, childName);
                if (found != null) {
                    return found;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the <see cref="GameObject"/>'s Static flag.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="includeChildren">If true, the Static flag will be set to it's children as well.</param>
        public static void SetStatic(this Transform transform, bool includeChildren = false) {
            transform.gameObject.isStatic = true;

            if (includeChildren) {
                var transforms = transform.GetComponentsInChildren<Transform>(true);
                foreach (var t in transforms) {
                    t.gameObject.isStatic = true;
                }
            }
        }
    }
}
