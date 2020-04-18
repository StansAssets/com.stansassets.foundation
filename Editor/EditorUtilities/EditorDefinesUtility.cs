using System;
using UnityEditor;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Provides method for managing the <see cref="PlayerSettings"/> script defines.
    /// </summary>
    public static class EditorDefinesUtility
    {
        /// <summary>
        /// Attempts to add a new #define constant to the Player Settings.
        /// </summary>
        /// <param name="newDefineCompileConstant">constant to attempt to define.</param>
        /// <param name="targets">platforms to add this for (default will add to all platforms).</param>
        public static void AddCompileDefine(string newDefineCompileConstant, params BuildTarget[] targets)
        {
            if (targets.Length == 0)
                targets = (BuildTarget[])Enum.GetValues(typeof(BuildTarget));

            foreach (BuildTarget target in targets)
            {
                var targetGroup = BuildPipeline.GetBuildTargetGroup(target);
                if (!IsBuildTargetSupported(targetGroup, target))
                    continue;

                if (targetGroup == BuildTargetGroup.Unknown) //the unknown group does not have any constants location
                    continue;

                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
                if (!defines.Contains(newDefineCompileConstant))
                {
                    if (defines.Length > 0) //if the list is empty, we don't need to append a semicolon first
                        defines += ";";
                    defines += newDefineCompileConstant;
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
                }
            }
        }

        /// <summary>
        /// Attempts to remove a #define constant from the Player Settings.
        /// </summary>
        /// <param name="defineCompileConstant">define constant.</param>
        /// <param name="targetGroups">platforms to add this for (default will add to all platforms).</param>
        public static void RemoveCompileDefine(string defineCompileConstant, params BuildTarget[] targetGroups)
        {
            if (targetGroups.Length == 0)
                targetGroups = (BuildTarget[])Enum.GetValues(typeof(BuildTarget));

            foreach (BuildTarget target in targetGroups)
            {
                var targetGroup = BuildPipeline.GetBuildTargetGroup(target);
                if (!IsBuildTargetSupported(targetGroup, target))
                    continue;

                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
                var index = defines.IndexOf(defineCompileConstant, StringComparison.CurrentCulture);
                if (index < 0)
                    continue; //this target does not contain the define

                if (index > 0)
                    index -= 1;

                //Remove the word and it's semicolon, or just the word (if listed last in defines)
                var lengthToRemove = Math.Min(defineCompileConstant.Length + 1, defines.Length - index);

                //remove the constant and it's associated semicolon (if necessary)
                defines = defines.Remove(index, lengthToRemove);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
            }
        }

        /// <summary>
        /// Check if define exists.
        /// </summary>
        /// <param name="defineCompileConstant">constant to attempt to define.</param>
        /// <param name="targetGroups">platforms to add this for (default will add to all platforms).</param>
        public static bool HasCompileDefine(string defineCompileConstant, params BuildTarget[] targetGroups)
        {
            if (targetGroups.Length == 0)
                targetGroups = (BuildTarget[])Enum.GetValues(typeof(BuildTarget));

            foreach (BuildTarget target in targetGroups)
            {
                var targetGroup = BuildPipeline.GetBuildTargetGroup(target);
                if (!IsBuildTargetSupported(targetGroup, target))
                    continue;

                // The unknown group does not have any constants location.
                if (targetGroup == BuildTargetGroup.Unknown)
                    continue;

                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
                if (!defines.Contains(defineCompileConstant))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Get user-specified symbols for script compilation for the current build target group
        /// </summary>
        public static string[] GetScriptingDefines()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var targetGroup = BuildPipeline.GetBuildTargetGroup(target);

            return GetScriptingDefines(targetGroup);
        }

        /// <summary>
        /// Get user-specified symbols for script compilation for the given build target group
        /// </summary>
        /// <param name="targetGroup">build target group</param>
        public static string[] GetScriptingDefines(BuildTargetGroup targetGroup)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            return defines.Split(';');
        }

        static bool IsBuildTargetSupported(BuildTargetGroup targetGroup, BuildTarget target)
        {
            return BuildPipeline.IsBuildTargetSupported(targetGroup, target);
        }
    }
}
