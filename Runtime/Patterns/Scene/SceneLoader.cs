using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StansAssets.Foundation.Patterns
{
    /// <summary>
    /// Provides methods to manage scenes loading.
    /// </summary>
    public static class SceneLoader
    {
        public static event Action<Scene> SceneUnloaded = delegate { };
        public static event Action<Scene, LoadSceneMode> SceneLoaded = delegate { };

        static readonly List<Scene> s_AdditiveScenes = new List<Scene>();
        static readonly Dictionary<string, AsyncOperation> s_LoadSceneOperations = new Dictionary<string, AsyncOperation>();
        static readonly Dictionary<string, List<Action<Scene>>> s_LoadSceneRequests = new Dictionary<string, List<Action<Scene>>>();
        static readonly Dictionary<string, List<Action>> s_UnloadSceneCallbacks = new Dictionary<string, List<Action>>();

        static SceneLoader()
        {
             SceneManager.sceneLoaded += AdditiveSceneLoaded;
             SceneManager.sceneUnloaded += SceneUnloadComplete;
        }

        /// <summary>
        /// Load Scene Additively by it's name.
        /// <param name="sceneName">Name of the scene to be loaded.</param>
        /// <param name="loadCompleted">Load Completed callback.</param>
        /// </summary>
        public static AsyncOperation LoadAdditively(string sceneName, Action<Scene> loadCompleted = null)
        {
            if (!s_LoadSceneRequests.ContainsKey(sceneName))
            {
                var callbacks = new List<Action<Scene>>();
                if (loadCompleted != null)
                    callbacks.Add(loadCompleted);


                var loadAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                s_LoadSceneRequests.Add(sceneName, callbacks);
                s_LoadSceneOperations.Add(sceneName, loadAsyncOperation);
                return loadAsyncOperation;
            }

            if (loadCompleted != null) {
                var callbacks = s_LoadSceneRequests[sceneName] ?? new List<Action<Scene>>();
                callbacks.Add(loadCompleted);
                s_LoadSceneRequests[sceneName] = callbacks;
            }

            return s_LoadSceneOperations[sceneName];
        }

        /// <summary>
        /// Current scene load async operation. Can be used to check if scene is loaded or current load progress.
        /// </summary>
        /// <param name="sceneName">Name of the scene.</param>
        public static AsyncOperation GetLoadProgress(string sceneName)
        {
            return s_LoadSceneOperations.TryGetValue(sceneName, out var asyncOperation)
                ? asyncOperation
                : null;
        }

        /// <summary>
        /// Unload scene.
        /// <param name="scene">The scene to be loaded.</param>
        /// <param name="unloadCompleted">Unload Completed callback.</param>
        /// </summary>
        public static void Unload(Scene scene, Action unloadCompleted = null)
        {
            Unload(scene.name, unloadCompleted);
        }

        /// <summary>
        /// Unload scene by name.
        /// <param name="sceneName">Name of the scene to be loaded.</param>
        /// <param name="unloadCompleted">Unload Completed callback.</param>
        /// </summary>
        public static void Unload(string sceneName, Action unloadCompleted = null)
        {
            if (!s_UnloadSceneCallbacks.ContainsKey(sceneName))
            {
                List<Action> callbacks = null;
                if (unloadCompleted != null)
                    callbacks = new List<Action> { unloadCompleted };

                s_UnloadSceneCallbacks.Add(sceneName, callbacks);

                for (var i = 0; i < s_AdditiveScenes.Count; i++)
                {
                    if (s_AdditiveScenes[i].name == sceneName)
                    {
                        s_AdditiveScenes.Remove(s_AdditiveScenes[i]);
                        break;
                    }
                }
                SceneManager.UnloadSceneAsync(sceneName);
            }
            else
            {
                var callbacks = s_UnloadSceneCallbacks[sceneName];
                if (unloadCompleted != null) {
                    if (callbacks == null)
                        callbacks = new List<Action>();
                    callbacks.Add(unloadCompleted);
                }
            }
        }

        static void AdditiveSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            s_AdditiveScenes.Add(scene);
            SceneLoaded.Invoke(scene, mode);

            if (s_LoadSceneRequests.TryGetValue(scene.name, out var callbacks))
            {
                foreach (var callback in callbacks)
                    callback(scene);
                s_LoadSceneRequests.Remove(scene.name);
            }
        }

        static void SceneUnloadComplete(Scene scene)
        {
            SceneUnloaded.Invoke(scene);
            if (s_UnloadSceneCallbacks.TryGetValue(scene.name, out var callbacks))
            {
                foreach (var callback in callbacks)
                    callback();

                s_UnloadSceneCallbacks.Remove(scene.name);
            }

            s_LoadSceneOperations.Remove(scene.name);
        }
    }
}
