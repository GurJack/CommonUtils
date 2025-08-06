using System.Drawing;

namespace CommonUtils.ImageProviders
{
    /// <summary>
    /// Interface for getting images from different sources.
    /// </summary>
    public interface IImageProvider
    {
        /// <summary>
        /// Gets image by name.
        /// </summary>
        /// <param name="name">The image name.</param>
        /// <returns>The image object.</returns>
        Image GetImage(string name);
        
        /// <summary>
        /// Gets icon by name.
        /// </summary>
        /// <param name="name">Icon name.</param>
        /// <returns>Icon object.</returns>
        Icon GetIcon(string name);
    }
}