using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace StansAssets.Foundation
{
    [CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
    public class InspectorButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var buttonAttribute = attribute as InspectorButtonAttribute;
            if (buttonAttribute == null)
            {
                return;
            }

            string methodName = buttonAttribute.MethodName;
            UnityEngine.Object target = property.serializedObject.targetObject;
            System.Type type = target.GetType();
            System.Reflection.MethodInfo method = type.GetMethod(methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (method == null)
            {
                GUI.Label(position, $"Method {methodName} could not be found");
                return;
            }

            if (method.GetParameters().Length > 0)
            {
                GUI.Label(position, "Method cannot have parameters.");
                return;
            }

            switch (buttonAttribute.Mode)
            {
                case InspectorButtonMode.OnlyEditorMode:
                    GUI.enabled = Application.isPlaying == false;
                    break;

                case InspectorButtonMode.OnlyPlayMode:
                    GUI.enabled = Application.isPlaying;
                    break;
            }

            if (GUI.Button(position, method.Name))
            {
                method.Invoke(target, null);
            }

            GUI.enabled = true;
        }
    }
}