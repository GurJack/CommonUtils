using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace CommonWPF.Extensions
{
    /// <summary>
    /// Color extensions
    /// </summary>
    public static class ColorExtension
    {
        /// <summary>
        /// Convert from System.Drawing.Color to System.Windows.Media.Color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static MColor ToMediaColor(this DColor color)
        {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Correct color by factor
        /// </summary>
        /// <returns></returns>
        public static DColor CorrectColor(this DColor color, float correctionFactor = 0.5f)
        {
            float red = (255 - color.R) * correctionFactor + color.R;
            float green = (255 - color.G) * correctionFactor + color.G;
            float blue = (255 - color.B) * correctionFactor + color.B;
            return DColor.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }

        /// <summary>Blends the specified colors together.</summary>
        /// <param name="color">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amount">How much of <paramref name="color"/> to keep, “on top of” <paramref name="backColor"/>.</param>
        /// <returns>The blended colors.</returns>
        public static DColor Blend(this DColor color, DColor backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return DColor.FromArgb(r, g, b);
        }

        /// <summary>Blends the specified colors together.</summary>
        /// <param name="color">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amount">How much of <paramref name="color"/> to keep, “on top of” <paramref name="backColor"/>.</param>
        /// <returns>The blended colors.</returns>
        public static DColor Blend(this DColor color, MColor backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return DColor.FromArgb(r, g, b);
        }

        /// <summary>Blends the specified colors together.</summary>
        /// <param name="color">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amount">How much of <paramref name="color"/> to keep, “on top of” <paramref name="backColor"/>.</param>
        /// <returns>The blended colors.</returns>
        public static MColor Blend(this MColor color, MColor backColor, double amount)
        {
            byte a = (byte)((color.A * amount) + backColor.A * (1 - amount));
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return MColor.FromArgb(a, r, g, b);
        }
    }
}