using UnityEngine;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Color Utility Methods.
    /// ColorUtility exists already, so we will call it Color helper.
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// Attempts to make a color struct from the html color string.
        /// If parsing is failed magenta color will be returned.
        ///
        /// Strings that begin with '#' will be parsed as hexadecimal in the following way:
        /// #RGB (becomes RRGGBB)
        /// #RRGGBB
        /// #RGBA (becomes RRGGBBAA)
        /// #RRGGBBAA
        ///
        /// When not specified alpha will default to FF.
        ///     Strings that do not begin with '#' will be parsed as literal colors, with the following supported:
        /// red, cyan, blue, darkblue, lightblue, purple, yellow, lime, fuchsia, white, silver, grey, black, orange, brown, maroon, green, olive, navy, teal, aqua, magenta..
        /// </summary>
        /// <param name="htmlString">Case insensitive html string to be converted into a color.</param>
        /// <returns>The converted color.</returns>
        public static Color MakeColorFromHtml(string htmlString)
        {
            return MakeColorFromHtml(htmlString, Color.magenta);
        }

        /// <summary>
        /// Attempts to make a color struct from the html color string.
        /// If parsing is failed <see cref="fallbackColor"/> color will be returned.
        ///
        /// Strings that begin with '#' will be parsed as hexadecimal in the following way:
        /// #RGB (becomes RRGGBB)
        /// #RRGGBB
        /// #RGBA (becomes RRGGBBAA)
        /// #RRGGBBAA
        ///
        /// When not specified alpha will default to FF.
        ///     Strings that do not begin with '#' will be parsed as literal colors, with the following supported:
        /// red, cyan, blue, darkblue, lightblue, purple, yellow, lime, fuchsia, white, silver, grey, black, orange, brown, maroon, green, olive, navy, teal, aqua, magenta..
        /// </summary>
        /// <param name="htmlString">Case insensitive html string to be converted into a color.</param>
        /// <param name="fallbackColor">Color to fall back to in case the parsing is failed.</param>
        /// <returns>The converted color.</returns>
        public static Color MakeColorFromHtml(string htmlString, Color fallbackColor)
        {
            return ColorUtility.TryParseHtmlString(htmlString, out var color) ? color : fallbackColor;
        }
    }
}
