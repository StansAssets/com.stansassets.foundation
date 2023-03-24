using System;
using UnityEngine;

namespace StansAssets.Foundation.UIElements
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public readonly string Name;

        public ButtonAttribute(string name = default)
        {
            Name = name;
        }
    }
}
