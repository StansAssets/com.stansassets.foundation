using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    public sealed class ButtonStrip : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ButtonStrip, UxmlTraits> {}
        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            public UxmlTraits()
            {
            }
        }

        public const string UssClassName = "stansassets-button-strip";
        const string k_ButtonClassName = UssClassName + "__button";

        const string k_ButtonLeftClassName = k_ButtonClassName + "--left";
        const string k_ButtonMidClassName = k_ButtonClassName + "--mid";
        const string k_ButtonRightClassName = k_ButtonClassName + "--right";
        const string k_ButtonIconClassName = UssClassName + "__button-icon";
        const string k_ButtonActiveClassName = k_ButtonClassName + "--active";

        readonly List<string> m_Choices = new List<string>();
        readonly List<string> m_Labels = new List<string>();
        readonly List<Button> m_Buttons = new List<Button>();

        public IEnumerable<string> Choices
        {
            get => m_Choices;
            set
            {
                m_Choices.Clear();

                if (value == null)
                    return;

                m_Choices.AddRange(value);

                RecreateButtons();
            }
        }

        public IEnumerable<string> Labels
        {
            get => m_Labels;
            set
            {
                m_Labels.Clear();

                if (value == null)
                    return;

                m_Labels.AddRange(value);

                RecreateButtons();
            }
        }

        public Action<EventBase> OnButtonClick { get; set; }

        public ButtonStrip() : this(new [] {"Left", "Middle", "Right"})
        {

        }

        public ButtonStrip(IEnumerable<string> choices)
        {
            AddToClassList(UssClassName);
            styleSheets.Add( Resources.Load<StyleSheet>("ButtonStrip"));

            /*

            if (EditorGUIUtility.isProSkin)
                styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(s_UssDarkPath));
            else
                styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(s_UssLightPath));
*/
            Choices = choices;
        }

        void RecreateButtons()
        {
            Clear();
            m_Buttons.Clear();
            for (int i = 0; i < m_Choices.Count; ++i)
            {
                var choice = m_Choices[i];
                string label = null;
                if (m_Labels.Count > i)
                    label = m_Labels[i];

                var button = new Button();
                button.AddToClassList(k_ButtonClassName);

                // Set button name for styling.
                button.name = choice;
                button.text = choice;

                // Set tooltip.
                button.tooltip = choice;

                if (i == 0)
                    button.AddToClassList(k_ButtonLeftClassName);
                else if (i == m_Choices.Count - 1)
                    button.AddToClassList(k_ButtonRightClassName);
                else
                    button.AddToClassList(k_ButtonMidClassName);

                button.clicked += () => { ToggleButtonStates(button);};
                m_Buttons.Add(button);

                if (OnButtonClick != null)
                    button.clickable.clickedWithEventInfo += OnButtonClick;
                Add(button);

                if (string.IsNullOrEmpty(label))
                {
                    var icon = new VisualElement();
                    icon.AddToClassList(k_ButtonIconClassName);
                    button.Add(icon);
                }
                else
                {
                    button.text = label;
                }
            }
        }

        void OnOptionChange(EventBase evt)
        {
            var button = evt.target as Button;
            ToggleButtonStates(button);
        }

        void ToggleButtonStates(Button button)
        {
            foreach (var btn in m_Buttons)
            {
                btn.RemoveFromClassList(k_ButtonActiveClassName);
            }

            button.AddToClassList(k_ButtonActiveClassName);
        }
    }
}
