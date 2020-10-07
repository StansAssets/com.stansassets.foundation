#if UNITY_2019_4_OR_NEWER
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    /// <summary>
    /// Public representation of unity UIElementsUtility class.
    /// </summary>
    public static class UIElementsUtility
    {
        /// <summary>
        /// Create <see cref="EventBase"/> from the correspondent <see cref="Event"/> instance.
        /// </summary>
        /// <param name="systemEvent">System event instance.</param>
        /// <returns>New  <see cref="EventBase"/> instance.</returns>
        public static EventBase CreateEvent(Event systemEvent)
        {
            return CreateEvent(systemEvent, systemEvent.rawType);
        }

        /// <summary>
        /// Create <see cref="EventBase"/> from the correspondent <see cref="Event"/> instance.
        /// </summary>
        /// <param name="systemEvent">System event instance.</param>
        /// <param name="eventType">System event type.</param>
        /// <returns>New  <see cref="EventBase"/> instance.</returns>
        public static EventBase CreateEvent(Event systemEvent, EventType eventType)
        {
            var uiElementsUtilityType = ReflectionUtility.FindType("UnityEngine.UIElements.UIElementsUtility");

            MethodInfo createEventMethodInfo = null;
            foreach (var method in uiElementsUtilityType.GetMethods( BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (method.Name.Equals("CreateEvent") && method.GetParameters().Length == 2)
                {
                    createEventMethodInfo = method;
                    break;
                }
            }

            return (EventBase) createEventMethodInfo?.Invoke(null, new object[]{systemEvent, eventType});
        }
    }
}
#endif
