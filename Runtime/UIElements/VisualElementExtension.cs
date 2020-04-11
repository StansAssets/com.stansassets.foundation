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
    }
}
