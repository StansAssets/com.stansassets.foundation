using System;
using System.Reflection;
using StansAssets.Foundation.UIElements;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class CustomEditorObject : UnityEditor.Editor
    {
        private MethodInfo[] m_methods;
        
        private void OnEnable()
        {
            var type = target.GetType();
            m_methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawInspectorButtons();
        }

        private void DrawInspectorButtons()
        {
            foreach (var method in m_methods)
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
