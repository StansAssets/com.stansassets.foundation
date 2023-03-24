using System.Reflection;
using StansAssets.Foundation.UIElements;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class ObjectCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawInspectorButtons();
        }

        private void DrawInspectorButtons()
        {
            var type = target.GetType();

            foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
                var parametersCount = method.GetParameters().Length;

                if (buttonAttribute != null && parametersCount == 0)
                {
                    if (GUILayout.Button(buttonAttribute.Name))
                    {
                        method.Invoke(target, null);
                        
                    }
                }
            }
        }
    }
}
