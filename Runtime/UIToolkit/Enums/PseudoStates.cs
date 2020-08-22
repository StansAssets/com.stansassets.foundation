using System;

namespace StansAssets.Foundation.UIElements
{
    /// <summary>
    /// Public representation of unity PseudoStates internal enum.
    /// </summary>
    [Flags]
    public enum PseudoStates
    {
        /// <summary>
        /// Control is currently pressed in the case of a button.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Mouse is over control, set and removed from dispatcher automatically.
        /// </summary>
        Hover = 1 << 1,

        /// <summary>
        /// Usually associated with toggles of some kind to change visible style.
        /// </summary>
        Checked = 1 << 3,

        /// <summary>
        /// Control will not respond to user input.
        /// </summary>
        Disabled = 1 << 5,

        /// <summary>
        /// Control has the keyboard focus. This is activated deactivated by the dispatcher automatically.
        /// </summary>
        Focus = 1 << 6,

        /// <summary>
        /// Set on the root visual element.
        /// </summary>
        Root = 1 << 7,
    }
}
