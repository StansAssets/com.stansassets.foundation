using UnityEngine;
using UnityEditor;

namespace StansAssets.Foundation.Editor.Shading
{
    /// <summary>
    /// Material property attribute that allows you to display the material property
    /// only if the specified material keywords are enabled.
    ///
    /// Use with **[ShowIf(KEYWORD_NAME, ...)]** before a shader property.
    /// Most often used in combination with the **[Toggle(KEYWORD_NAME)]** attribute.
    /// </summary>
    /// <example>
    /// Use example:
    /// <code>
    ///     [Toggle(_BASE_COLOR_ON)] _ToggleBaseColor("Color", Int) = 0
	///     [ShowIf(_BASE_COLOR_ON)] _BaseColor("", Color) = (1,1,1,1)
    /// </code>
    /// </example>
    public class ShowIfDrawer : MaterialPropertyDrawer
    {
        readonly string[] m_RequiredKeywords;
        bool m_IsElementHidden;

        /// <summary>
        /// Creates ShowIfDrawer with one required keyword.
        /// </summary>
        public ShowIfDrawer(
            string keyword0)
        {
            m_RequiredKeywords = new[] {keyword0};
        }

        /// <summary>
        /// Creates ShowIfDrawer with two required keywords.
        /// </summary>
        public ShowIfDrawer(
            string keyword0,
            string keyword1)
        {
            m_RequiredKeywords = new[] {keyword0, keyword1};
        }

        /// <summary>
        /// Creates ShowIfDrawer with three required keywords.
        /// </summary>
        public ShowIfDrawer(
            string keyword0,
            string keyword1,
            string keyword2)
        {
            m_RequiredKeywords = new[] {keyword0, keyword1, keyword2};
        }

        /// <summary>
        /// Creates ShowIfDrawer with four required keywords.
        /// </summary>
        public ShowIfDrawer(
            string keyword0,
            string keyword1,
            string keyword2,
            string keyword3)
        {
            m_RequiredKeywords = new[] {keyword0, keyword1, keyword2, keyword3};
        }

        public override void OnGUI(
            Rect position,
            MaterialProperty prop,
            string label,
            MaterialEditor editor)
        {
            m_IsElementHidden = false;

            foreach (var target in editor.targets)
            {
                var mat = target as Material;

                if (mat != null)
                {
                    foreach (var keyword in m_RequiredKeywords)
                    {
                        if (!mat.IsKeywordEnabled(keyword))
                        {
                            m_IsElementHidden = true;
                            break;
                        }
                    }
                }
            }

            if (!m_IsElementHidden)
            {
                editor.DefaultShaderProperty(prop, label);
            }
        }

        /// <summary>
        /// Will always return <c>0</c>
        /// </summary>
        /// <param name="prop">The <see cref="MaterialProperty"/> to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        /// <param name="editor">Current material editor.</param>
        /// <returns>Property height in pixels</returns>
        public override float GetPropertyHeight (
            MaterialProperty prop,
            string label,
            MaterialEditor editor)
        {
            return 0;
        }
    }
}