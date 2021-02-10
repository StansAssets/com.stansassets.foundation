using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// AssetDatabase utility methods.
    /// </summary>
    public static class AssetDatabaseUtility
    {
        /// <summary>
        /// Gets the local Identifier In File, for the given GameObject
        /// Return 0 in case Game Object wasn't yet saved
        /// </summary>
        /// <param name="unityObject">GameObject tou want to check</param>
        public static int GetLocalIdentifierInFile(Object unityObject)
        {
            var inspectorModeInfo = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            if (inspectorModeInfo == null)
                throw new InvalidOperationException("Failed to obtain the inspectorMode property.");

            var serializedObject = new SerializedObject(unityObject);
            inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);
            var localIdProp = serializedObject.FindProperty("m_LocalIdentfierInFile"); //note the misspelling!
            return localIdProp.intValue;
        }

        /// <summary>
        /// Returns the absolute path name for the asset.
        /// </summary>
        /// <param name="asset">A reference to the asset.</param>
        /// <returns>The asset path name, or null, or an empty string if the asset does not exist.</returns>
        public static string GetAssetAbsolutePath(Object asset)
        {
            var absoluteFilePath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);
            return $"{absoluteFilePath}/{AssetDatabase.GetAssetPath(asset)}";
        }
    }
}
