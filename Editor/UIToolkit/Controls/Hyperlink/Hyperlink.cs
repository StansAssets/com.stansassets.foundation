#if UNITY_2019_4_OR_NEWER
using JetBrains.Annotations;
using StansAssets.Foundation.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Foundation.UIElements
{
    public class Hyperlink : BindableElement
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<Hyperlink, UxmlTraits> { }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription m_Url = new UxmlStringAttributeDescription { name = "url" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((Hyperlink)ve).Url = m_Url.GetValueFromBag(bag, cc);
            }
        }

        public const string USSClassName = "stansassets-hyperlink-block";
        public const string HeaderUssClassName = USSClassName + "__header";
        public const string ContentUssClassName = USSClassName + "__content";

        string m_Url;
        readonly VisualElement m_Container;

        public override VisualElement contentContainer => m_Container;

        public string Url
        {
            get => m_Url;
            set => m_Url = value;
        }

        public Hyperlink()
        {
            AddToClassList(USSClassName);
            
            var button = new Button()
            {
                name = "header",
            };
            button.clicked += () => { Application.OpenURL(m_Url); };
            button.AddToClassList(HeaderUssClassName);
            
            m_Container = new VisualElement()
            {
                name = "content",
            };
            m_Container.AddToClassList(ContentUssClassName);
            
            button.Add(m_Container);
            
            hierarchy.Add(button);

            UIToolkitEditorUtility.ApplyStyleForInternalControl(this, nameof(Hyperlink));
        }
    }
}
#endif
