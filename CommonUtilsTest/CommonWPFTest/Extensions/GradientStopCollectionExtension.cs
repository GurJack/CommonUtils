using System.Linq;
using System.Windows.Media;

namespace CommonWPF.Extensions
{
    /// <summary>
    /// GradientStopCollection extensions.
    /// </summary>
    public static class GradientStopCollectionExtension
    {
        /// <summary>
        /// Gets color for specified offset.
        /// </summary>
        /// <param name="gsc"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Color GetRelativeColor(this GradientStopCollection gsc, double offset)
        {
            GradientStop before = gsc.First(w => w.Offset == gsc.Min(m => m.Offset));
            GradientStop after = gsc.First(w => w.Offset == gsc.Max(m => m.Offset));

            foreach (var gs in gsc)
            {
                if (gs.Offset == offset) return gs.Color; //new line added

                if (gs.Offset < offset && gs.Offset > before.Offset)
                {
                    before = gs;
                }
                if (gs.Offset > offset && gs.Offset < after.Offset)
                {
                    after = gs;
                }
            }

            var color = new Color();

            color.ScA = (float)((offset - before.Offset) * (after.Color.ScA - before.Color.ScA) / (after.Offset - before.Offset) + before.Color.ScA);
            color.ScR = (float)((offset - before.Offset) * (after.Color.ScR - before.Color.ScR) / (after.Offset - before.Offset) + before.Color.ScR);
            color.ScG = (float)((offset - before.Offset) * (after.Color.ScG - before.Color.ScG) / (after.Offset - before.Offset) + before.Color.ScG);
            color.ScB = (float)((offset - before.Offset) * (after.Color.ScB - before.Color.ScB) / (after.Offset - before.Offset) + before.Color.ScB);

            return color;
        }
    }
}