using UnityEngine;

namespace Karma.Extensions
{
    public static class ColorExtensions
    {
        public static Color Lerp(this Color color1, Color color2, float t)
        {
            return new Color(
                Mathf.Lerp(color1.r, color2.r, t),
                Mathf.Lerp(color1.g, color2.g, t),
                Mathf.Lerp(color1.b, color2.b, t),
                Mathf.Lerp(color1.a, color2.a, t)
            );
        }
        
        public static Color WithAlpha(this Color color, float? alpha = null)
        {
            return new Color(color.r, color.g, color.b, alpha ?? color.a);
        }
        
        public static Color AddAlpha(this Color color, float? alpha = null)
        {
            return new Color(color.r, color.g, color.b, color.a + (alpha ?? 0));
        }
        
        public static Color MultiplyAlpha(this Color color, float? alpha = null)
        {
            return new Color(color.r, color.g, color.b, color.a * (alpha ?? 1));
        }
        
    }
}