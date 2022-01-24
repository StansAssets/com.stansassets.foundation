using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StansAssets.Foundation.Async
{
    [InitializeOnLoad]
    public class EditorCoroutineUtility
    {
        private static List<IEnumerator> EditorCoroutine = new List<IEnumerator>();

        public static IEnumerator StartEditorCoroutine(IEnumerator newCor)
        {
            EditorCoroutine.Add(newCor);
            return newCor;
        }

        static EditorCoroutineUtility()
        {
            EditorApplication.update += ExecuteCoroutine;
        }

        static int currentExecute = 0;

        private static void ExecuteCoroutine()
        {
            if (EditorCoroutine.Count <= 0)
            {
                return;
            }

            currentExecute = (currentExecute + 1) % EditorCoroutine.Count;

            bool finish = !EditorCoroutine[currentExecute].MoveNext();

            if (finish)
            {
                EditorCoroutine.RemoveAt(currentExecute);
            }
        }
    }
}