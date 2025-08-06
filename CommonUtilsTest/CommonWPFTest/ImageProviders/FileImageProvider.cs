using System;
using System.Drawing;
using System.IO;
using CommonUtils.Helpers;

namespace CommonUtils.ImageProviders
{
    /// <summary>
    /// Class for getting images from files by <see cref="FileProvider"/>.
    /// </summary>
    public class FileImageProvider : IImageProvider
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FileImageProvider()
        {
        }

        /// <summary>
        /// Constructor with a root directory name.
        /// </summary>
        /// <param name="rootPath">The root directory for images.</param>
        public FileImageProvider(string rootPath)
        {
            if (rootPath == null)
            {
                throw new ArgumentNullException(nameof(rootPath));
            }

            RootPath = rootPath.Replace(Path.DirectorySeparatorChar, UriHelper.PathSeparator);
        }


        /// <summary>
        /// Gets the root directory name.
        /// </summary>
        public string RootPath { get; } = String.Empty;

        /// <summary>
        /// Gets image by name.
        /// </summary>
        /// <param name="name">The image name.</param>
        /// <returns>The image object.</returns>
        public Image GetImage(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var fileName = GetFullPath(name);

            if (!FileProvider.ExistsFile(fileName))
            {
                return null;
            }

            return ImageHelper.ByteArrayToImage(FileProvider.ReadBytes(fileName));
        }

        /// <summary>
        /// Gets icon by name.
        /// </summary>
        /// <param name="name">Icon name.</param>
        /// <returns>Icon object.</returns>
        public Icon GetIcon(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var fileName = GetFullPath(name);

            if (!FileProvider.ExistsFile(fileName))
            {
                return null;
            }

            var content = FileProvider.ReadBytes(fileName);
            using (var stream = new MemoryStream(content))
            {
                return new Icon(stream);
            }
        }

        private string GetFullPath(string path)
        {
            path = path.Replace(Path.DirectorySeparatorChar, UriHelper.PathSeparator);
            return UriHelper.Combine(RootPath, UriHelper.GetTail(path));
        }
    }
}