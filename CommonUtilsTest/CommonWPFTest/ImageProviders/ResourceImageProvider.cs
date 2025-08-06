using System;
using System.Drawing;
using System.Reflection;
using CommonUtils.Helpers;

namespace CommonUtils.ImageProviders
{
    /// <summary>
    /// Class for getting images from an assembly resources base on https://msdn.microsoft.com/en-us/library/aa287676(v=vs.71).aspx />.
    /// All images need to mark as an embedded resource!
    /// </summary>
    public class ResourceImageProvider : IImageProvider
    {
        /// <summary>
        /// Constructor with the assembly.
        /// </summary>
        /// <param name="resourceAssembly">Specified assembly.</param>
        public ResourceImageProvider(Assembly resourceAssembly) : this(resourceAssembly, String.Empty)
        {
        }

        /// <summary>
        /// Constructor with the assembly and the root resource path.
        /// </summary>
        /// <param name="resourceAssembly">Specified assembly.</param>
        /// <param name="rootPath">Specified root resource path.</param>
        public ResourceImageProvider(Assembly resourceAssembly, string rootPath)
        {
            ResourceAssembly = resourceAssembly ?? throw new ArgumentNullException(nameof(resourceAssembly));
            RootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
        }

        /// <summary>
        /// Gets the resource assembly.
        /// </summary>
        public Assembly ResourceAssembly { get; }

        /// <summary>
        /// Gets the root resource path.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets image by name.
        /// </summary>
        /// <param name="name">The image name.</param>
        /// <returns>The image object.</returns>
        public virtual Image GetImage(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = UriHelper.GetTail(name);

            var stream = ResourceAssembly.GetManifestResourceStream(GetFullPath(name));
            if (stream == null)
            {
                return null;
            }

            return Image.FromStream(stream);
        }

        /// <summary>
        /// Gets icon by name.
        /// </summary>
        /// <param name="name">Icon name.</param>
        /// <returns>Icon object.</returns>
        public virtual Icon GetIcon(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = UriHelper.GetTail(name);

            var stream = ResourceAssembly.GetManifestResourceStream(GetFullPath(name));
            if (stream == null)
            {
                return null;
            }

            return new Icon(stream);
        }

        private string GetFullPath(string path)
        {
            if (RootPath.Length == 0)
            {
                return path;
            }

            return RootPath + "." + path;
        }
    }
}