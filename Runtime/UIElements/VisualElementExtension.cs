﻿#if UNITY_2019_1_OR_NEWER
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    /// <summary>
    /// VisualElement Utility Methods.
    /// </summary>
    public static class VisualElementExtension
    {
        /// <summary>
        /// Set PseudoState into visualElement to mimic input behaviour.
        /// </summary>
        /// <param name="visualElement">VisualElement instance.</param>
        /// <param name="state">Pseudo state of a VisualElement: Active, Hover, Checked, Disabled, Focus, Root.</param>
        public static void SetPseudoState(this VisualElement visualElement, PseudoStates state)
        {
            int intState = (int)state;
            var property = typeof(VisualElement).GetProperty("pseudoStates", BindingFlags.Instance | BindingFlags.NonPublic);
            property?.SetMethod?.Invoke(visualElement, new object[] { intState });
        }

        /// <summary>
        /// Returns PseudoState of a visualElement.
        /// </summary>
        /// <param name="visualElement">VisualElement instance.</param>
        /// <param name="state">Pseudo state of a VisualElement: Active, Hover, Checked, Disabled, Focus, Root.</param>
        public static PseudoStates GetPseudoState(this VisualElement visualElement)
        {
            var property = typeof(VisualElement).GetProperty("pseudoStates", BindingFlags.Instance | BindingFlags.NonPublic);
            var res= property?.GetMethod?.Invoke(visualElement, new object[] { });
            int intResult = (int?)res ?? -1;
            return (PseudoStates)intResult;
        }

        /// <summary>
        /// Traverses up the hierarchy to find all of the parent instances of type T.
        /// </summary>
        /// <param name="element">Current element to search parents.</param>
        /// <typeparam name="T">Type which you want to find</typeparam>
        /// <returns>Collection of T instances found.</returns>
        public static IEnumerable<T> GetParentsOfType<T>(this VisualElement element) where T : VisualElement
        {
            var result = new List<T>();

            var parent = element;
            while (parent != null)
            {
                if (parent is T selected)
                    result.Add(selected);

                parent = parent.parent;
            }

            return result;
        }

        /// <summary>
        /// Traverses up the hierarchy to find first parent instance of type T.
        /// </summary>
        /// <param name="element">Current element to search parent.</param>
        /// <typeparam name="T">Type which you want to find</typeparam>
        /// <returns>T instance found</returns>
        public static T GetFirstParentOfType<T>(this VisualElement element) where T : VisualElement
        {
            return GetParentsOfType<T>(element).FirstOrDefault();
        }
    }
}
#endif
