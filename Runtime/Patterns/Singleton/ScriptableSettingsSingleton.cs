#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

using UnityEngine;

namespace  StansAssets.Foundation
{
    /// <summary>
    /// This class simplifies a singleton pattern implementation,
    /// that can be used with classes extended from a ScriptableObject.
    /// </summary>
    public abstract class ScriptableSettingsSingleton<T> : ScriptableSettings where T : ScriptableSettings
    {
        static T s_Instance;

        /// <summary>
        /// Returns a singleton class instance
        /// If current instance is not assigned it will try to find an object of the instance type,
        /// in case instance already exists in a project. If not, new instance will be created,
        /// and saved under a <see cref="Instance.BasePath"/> location
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = Resources.Load(typeof(T).Name) as T;
                    if (s_Instance == null)
                    {
                        s_Instance = CreateInstance<T>();
                        SaveToAssetDatabase(s_Instance);
                    }
                }
                return s_Instance;
            }
        }

        /// <summary>
        /// Saves instance to an editor database.
        /// </summary>
        public static void Save()
        {

#if UNITY_EDITOR
            //TODO use Undo
            EditorUtility.SetDirty(Instance);
#endif
        }

        /// <summary>
        /// Settings version.
        /// </summary>
        public static string Version => Instance.VersionCode;

        /// <summary>
        /// Method will update and save version code
        /// Returns <c>true</c> in case version was update and <c>false</c> if version remained the same.
        /// This method is meant to be used for an internal version check, to foldout if plugin was updated to a new version
        /// and run the necessary actions.
        /// </summary>
        public static bool UpdateVersion(string versionCode)
        {
            if (string.IsNullOrEmpty(Version)) {
                Instance.VersionCode = versionCode;
                Save();
                return true;
            }

            if(!Version.Equals(versionCode)) {
                Instance.VersionCode = versionCode;
                Save();
                return true;
            }

            return false;
        }

        static void SaveToAssetDatabase(T asset) {
#if UNITY_EDITOR
            var path = $"{Instance.BasePath}{asset.GetType().Name}.asset";
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            AssetDatabase.CreateAsset(asset, path);
#endif
        }
    }
}