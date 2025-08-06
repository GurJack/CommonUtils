using System;
using System.Windows.Media;

namespace CommonWPF.Extensions
{
    /// <summary>
    /// Brush (System.Windows.Media) extensions.
    /// </summary>
    public static class BrushExtension
    {
        /// <summary>
        /// Converts to color.
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public static Color? ToColor(this Brush brush)
        {
            if (brush == null)
                return null;

            if (brush is SolidColorBrush)
                return ((SolidColorBrush) brush).Color;

            if (brush is LinearGradientBrush)
            {
                return ((LinearGradientBrush) brush).GradientStops.GetRelativeColor(0.5);
            }

            throw new ArgumentException($"Incorrent type of brush: <{brush.GetType().Name}>.");
        }
    }

    
}