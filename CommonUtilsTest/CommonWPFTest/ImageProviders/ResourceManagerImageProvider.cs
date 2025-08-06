using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using CommonUtils.Helpers;

namespace CommonUtils.ImageProviders
{
    /// <summary>
    /// Class for getting images from an assembly resources through <see cref="ResourceManager"/>.
    /// </summary>
    public sealed class ResourceManagerImageProvider : ResourceImageProvider
    {
        /// <summary>
        /// Constructor with the assembly.
        /// </summary>
        /// <param name="resourceAssembly">Specified assembly.</param>
        public ResourceManagerImageProvider(Assembly resourceAssembly) : base(resourceAssembly)
        {
        }

        /// <summary>
        /// Constructor with the assembly and the root resource path.
        /// </summary>
        /// <param name="resourceAssembly">Specified assembly.</param>
        /// <param name="rootPath">Specified root resource path.</param>
        public ResourceManagerImageProvider(Assembly resourceAssembly, string rootPath) : base(resourceAssembly, rootPath)
        {
            ResourceManager = new ResourceManager(rootPath, resourceAssembly);
        }

        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        public ResourceManager ResourceManager { get; }

        /// <summary>
        /// Gets image by name.
        /// </summary>
        /// <param name="name">The image name.</param>
        /// <returns>The image object.</returns>
        public override Image GetImage(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = UriHelper.GetTail(name);

            return (Image) ResourceManager.GetObject(name);
        }

        /// <summary>
        /// Gets icon by name.
        /// </summary>
        /// <param name="name">Icon name.</param>
        /// <returns>Icon object.</returns>
        public override Icon GetIcon(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = UriHelper.GetTail(name);

            return (Icon) ResourceManager.GetObject(name);
        }
    }
}