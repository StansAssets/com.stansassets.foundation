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
    /// 
    /// Usage example:
    ///     [Toggle(_BASE_COLOR_ON)] _ToggleBaseColor("Color", Int) = 0
	///     [ShowIf(_BASE_COLOR_ON)] _BaseColor("", Color) = (1,1,1,1)
    /// </summary>
    class ShowIfDrawer : MaterialPropertyDrawer
    {
        string[] m_RequiredKeywords;
        bool m_IsElementHidden;
 
        /// <summary>
        /// Creates ShowIfDrawer with one required keyword.
        /// </summary>
        public ShowIfDrawer(
            string keyword0)
        {
            m_RequiredKeywords = new string[] {keyword0};
        }
 
        /// <summary>
        /// Creates ShowIfDrawer with two required keywords.
        /// </summary>
        public ShowIfDrawer(
            string keyword0,
            string keyword1)
        {
            m_RequiredKeywords = new string[] {keyword0, keyword1};
        }
 
        /// <summary>
        /// Creates ShowIfDrawer with three required keywords.
        /// </summary>
        public ShowIfDrawer(
            string keyword0,
            string keyword1,
            string keyword2)
        {
            m_RequiredKeywords = new string[] {keyword0, keyword1, keyword2};
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
            m_RequiredKeywords = new string[] {keyword0, keyword1, keyword2, keyword3};
        }
 
        public override void OnGUI(
            Rect position,
            MaterialProperty prop,
            string label,
            MaterialEditor editor)
        {
            m_IsElementHidden = false;

            for (int i = 0; i < editor.targets.Length; ++i)
            {
                Material mat = editor.targets[i] as Material;

                if (mat != null)
                {
                    for (int j = 0; j < m_RequiredKeywords.Length; ++j)
                    {
                        if (!mat.IsKeywordEnabled(m_RequiredKeywords[j]))
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

        public override float GetPropertyHeight (
            MaterialProperty prop,
            string label,
            MaterialEditor editor)
        { 
            return 0;
        }
    }
}