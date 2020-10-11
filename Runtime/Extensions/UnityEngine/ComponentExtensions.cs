using System.Reflection;
using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// Unity `Component` extension methods.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Gets the local Identifier In File, for the given Component
        /// Return 0 in case Component wasn't yet saved
        /// </summary>
        /// <param name="go">Component you want to check</param>
        public static int GetLocalIdentifierInFile(Component go)
        {
#if UNITY_EDITOR
            var inspectorModeInfo = typeof(UnityEditor.SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            var serializedObject = new UnityEditor.SerializedObject(go);
            inspectorModeInfo.SetValue(serializedObject, UnityEditor.InspectorMode.Debug, null);
            var localIdProp = serializedObject.FindProperty("m_LocalIdentfierInFile"); //note the misspelling!
            return localIdProp.intValue;
#else
            return 0;
#endif
        }
    }
}
