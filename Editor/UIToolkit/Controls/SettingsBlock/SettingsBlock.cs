#if UNITY_2019_4_OR_NEWER
using JetBrains.Annotations;
using StansAssets.Foundation.Editor;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    /// <summary>
    /// The layout control that created a block with bold settings title and 10px intended content.
    /// </summary>
    public class SettingsBlock : BindableElement
    {
        [UsedImplicitly]
        internal new class UxmlFactory : UxmlFactory<SettingsBlock, UxmlTraits> { }

        internal new class UxmlTraits : BindableElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription { name = "label" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((SettingsBlock)ve).Label = m_Label.GetValueFromBag(bag, cc);
            }
        }

        /// <summary>
        /// Control Uss class name.
        /// </summary>
        public const string USSClassName = "stansassets-settings-block";
        
        /// <summary>
        /// Header Uss class name.
        /// </summary>
        public const string HeaderUssClassName = USSClassName + "__header";
        
        /// <summary>
        /// Content Uss class name.
        /// </summary>
        public const string ContentUssClassName = USSClassName + "__content";

        readonly Label m_Label;
        readonly VisualElement m_Container;

        public override VisualElement contentContainer => m_Container;

        /// <summary>
        /// Block header label.
        /// </summary>
        public string Label
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        /// <summary>
        /// Creates new settings block.
        /// </summary>
        public SettingsBlock()
        {
            AddToClassList(USSClassName);
            UIToolkitEditorUtility.ApplyStyleForInternalControl(this, nameof(SettingsBlock));
            var header = new VisualElement()
            {
                name = "header",
            };
            header.AddToClassList(HeaderUssClassName);
            hierarchy.Add(header);

            m_Label = new Label();
            header.Add(m_Label);

            m_Container = new VisualElement()
            {
                name = "content",
            };

            m_Container.AddToClassList(ContentUssClassName);
            hierarchy.Add(m_Container);
        }
    }
}

#endif
