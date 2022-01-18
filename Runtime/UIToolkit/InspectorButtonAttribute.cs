using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StansAssets.Foundation
{
    public enum InspectorButtonMode
    {
        AllModes,
        OnlyPlayMode,
        OnlyEditorMode,
    }

    public class InspectorButtonAttribute : PropertyAttribute
    {
        public string MethodName { get; }
        public InspectorButtonMode Mode { get; }

        public InspectorButtonAttribute(string methodName, InspectorButtonMode mode = InspectorButtonMode.AllModes)
        {
            Mode = mode;
            MethodName = methodName;
        }
    }
}