using UnityEngine;

namespace Karma.Extensions
{
    public static class StringExtensions
    {
        public static string SetColor(this string text, string color)
        {
            return $"<color={color}>{text}</color>";
        }
        public static string SetColor(this string text, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";
        }
        public static string Bold(this string text)
        {
            return $"<b>{text}</b>";
        }
        public static string Italic(this string text)
        {
            return $"<i>{text}</i>";
        }
        public static string SetSize(this string text, int size)
        {
            return $"<size={size}>{text}</size>";
        }
        public static string SetAlpha(this string text, float alpha)
        {
            return $"<alpha={alpha}>{text}</alpha>";
        }
        public static string SetCharacterSpacing(this string text, float spacing)
        {
            return $"<cspace={spacing}>{text}</cspace>";
        }
    }
}