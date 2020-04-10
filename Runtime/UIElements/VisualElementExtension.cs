using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    /// <summary>
    /// Public representation of internal VisualElement API.
    /// </summary>
    public static class VisualElementExtension
    {
        [Flags]
        public enum PseudoStates
        {
            Active    = 1 << 0,     // control is currently pressed in the case of a button
            Hover     = 1 << 1,     // mouse is over control, set and removed from dispatcher automatically
            Checked   = 1 << 3,     // usually associated with toggles of some kind to change visible style
            Disabled  = 1 << 5,     // control will not respond to user input
            Focus     = 1 << 6,     // control has the keyboard focus. This is activated deactivated by the dispatcher automatically
            Root      = 1 << 7,     // set on the root visual element
        }

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
