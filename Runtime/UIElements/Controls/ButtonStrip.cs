using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    public sealed class ButtonStrip : VisualElement
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<ButtonStrip, UxmlTraits> {}
        public new class UxmlTraits : BindableElement.UxmlTraits { }

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

        public IEnumerable<string> Choices => m_Choices;
        public IEnumerable<string> Labels => m_Labels;

        readonly TextField m_TextField;

        public string Value { get; set; }
        public Action OnButtonClick { get; set; }

        public ButtonStrip() : this(new [] {"LEFT", "MIDDLE", "RIGHT"})
        {

        }

        public ButtonStrip(IEnumerable<string> choices)
        {
            AddToClassList(UssClassName);
            styleSheets.Add( Resources.Load<StyleSheet>("ButtonStrip"));

            var collection = choices.ToList();
            m_Choices.AddRange(collection);
            m_Labels.AddRange(collection);
            RecreateButtons();

            m_TextField = new TextField { viewDataKey = "btn-data" };
            m_TextField.style.display = DisplayStyle.None;
            Add(m_TextField);

            // This is only possible when data is restored.
            m_TextField.RegisterValueChangedCallback(e =>
            {
                SetValue(e.newValue);
            });
        }

        public void SetValue(string value)
        {
            SetValueWithoutNotify(value);
            OnButtonClick?.Invoke();
        }

        public void SetValueWithoutNotify(string value)
        {
            var index = m_Choices.IndexOf(value);
            if(index == -1)
                return;

            Value = value;
            m_TextField.SetValueWithoutNotify(value);
            ToggleButtonStates(m_Buttons[index]);
        }

        public void AddChoice(string choice, string label)
        {
            m_Choices.Add(choice);
            m_Labels.Add(label);
            RecreateButtons();

            if(m_Choices.Count == 1)
                SetValueWithoutNotify(choice);
        }

        public void CleanUp()
        {
            foreach (var button in m_Buttons)
                button.RemoveFromHierarchy();

            m_Choices.Clear();
            m_Labels.Clear();
            m_Buttons.Clear();
        }

        void RecreateButtons()
        {
            foreach (var button in m_Buttons)
                button.RemoveFromHierarchy();

            m_Buttons.Clear();
            for (var i = 0; i < m_Choices.Count; ++i)
            {
                var choice = m_Choices[i];
                string label = null;
                if (m_Labels.Count > i)
                    label = m_Labels[i];

                var button = new Button();
                button.AddToClassList(k_ButtonClassName);

                // Set button name for styling.
                button.name = choice;

                // Set tooltip.
                button.tooltip = choice;

                if (i == 0)
                    button.AddToClassList(k_ButtonLeftClassName);
                else if (i == m_Choices.Count - 1)
                    button.AddToClassList(k_ButtonRightClassName);
                else
                    button.AddToClassList(k_ButtonMidClassName);

                button.clicked += () =>
                {
                    SetValue(choice);
                };

                m_Buttons.Add(button);

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

            SetValueWithoutNotify(Value);
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
