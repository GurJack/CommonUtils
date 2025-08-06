using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using CommonUtils.Helpers;
using CommonUtils.Resources;


namespace CommonUtils.ImageProviders
{
    /// <summary>
    /// Common image provider
    /// </summary>
    public static class ImageProvider
    {
        /// <summary>
        /// The base image provider URI prefix.
        /// </summary>
        public const string Base = "";

        /// <summary>
        /// The common image provider URI prefix.
        /// </summary>
        public const string Common = "common://";

        /// <summary>
        /// The file image provider URI prefix.
        /// </summary>
        public const string File = "file://";

        /// <summary>
        /// The store image provider URI prefix. Images store into datasource, e.g. database.
        /// </summary>
        public const string Storage = "storage://";

        /// <summary>
        /// Simple IoC container for <see cref="IImageProvider"/> instances.
        /// The key is URI prefix.
        /// </summary>
        private static readonly Dictionary<string, IImageProvider> ImageProviderHash = new Dictionary<string, IImageProvider>();
        private static readonly Dictionary<string, Image> ImageHash = new Dictionary<string, Image>();
        private static readonly Dictionary<string, Icon> IconHash = new Dictionary<string, Icon>();

#if NET461 || NET47 || NET471 || NET472
        private static readonly Dictionary<string, System.Windows.Media.Imaging.BitmapSource> ImageSourceHash = new Dictionary<string, System.Windows.Media.Imaging.BitmapSource>();
#endif

        /// <summary>
        /// Static constructor.
        /// Sets two default image providers:
        /// the base resource images with a "" prefix
        /// and for the file images with a "file://" prefix.
        /// </summary>
        static ImageProvider()
        {
            var asm = Assembly.GetExecutingAssembly();
            SetImageProvider(Base, new ResourceManagerImageProvider(asm, typeof(ImageProvider).Namespace + ".Resources.Images"));
            //SetImageProvider(Common, new ResourceImageProvider(asm, asm.GetName().Name + ".Resources.Images"));
            SetImageProvider(File, new FileImageProvider());
        }

        /// <summary>
        /// Gets image provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="IImageProvider"/>.</returns>
        public static IImageProvider GetImageProvider(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((ICollection) ImageProviderHash).SyncRoot)
            {
                return ImageProviderHash[prefix];
            }
        }

        /// <summary>
        /// Sets image provider with specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <param name="provider">Implementation of <see cref="IImageProvider"/>.</param>
        public static void SetImageProvider(string prefix, IImageProvider provider)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((ICollection) ImageProviderHash).SyncRoot)
            {
                if (provider == null)
                {
                    RemoveImageProvider(prefix);
                }
                else
                {
                    ImageProviderHash[prefix] = provider;
                }
            }
        }

        /// <summary>
        /// Removes image provider with the specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="IImageProvider"/>.</returns>
        public static void RemoveImageProvider(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((ICollection) ImageProviderHash).SyncRoot)
            {
                ImageProviderHash.Remove(prefix);
            }
        }

        /// <summary>
        /// Gets image provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="IImageProvider"/>.</returns>
        private static IImageProvider GetImageProviderByPath(string path)
        {
            return GetImageProvider(UriHelper.GetPrefix(path));
        }

        /// <summary>
        /// Gets image by name.
        /// </summary>
        /// <param name="name">The image name.</param>
        /// <returns>The image object.</returns>
        public static Image GetImage(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            lock (((ICollection) ImageHash).SyncRoot)
            {
                if (ImageHash.TryGetValue(name, out Image image))
                {
                    return image;
                }

                var imageProvider = GetImageProviderByPath(name);
                if (imageProvider == null)
                {
                    throw new ArgumentException(StringHelper.FormatMessage(CommonMessages.ImageProviderNotFound, name));
                }

                lock (imageProvider)
                {
                    var result = imageProvider.GetImage(name);
                    if (result == null)
                    {
                        return null;
                    }

                    ImageHash.Add(name, result);
                    return result;
                }
            }
        }

#if NET461 || NET47 || NET471 || NET472
        /// <summary>
        /// Gets image source by name.
        /// </summary>
        /// <param name="name">The image name.</param>
        /// <returns>The image source object.</returns>
        public static System.Windows.Media.Imaging.BitmapSource GetImageSource(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            lock (((ICollection) ImageSourceHash).SyncRoot)
            {
                if (ImageSourceHash.TryGetValue(name, out var image))
                {
                    return image;
                }

                var imageProvider = GetImageProviderByPath(name);
                if (imageProvider == null)
                {
                    throw new ArgumentException(StringHelper.FormatMessage(CommonMessages.ImageProviderNotFound, name));
                }

                lock (imageProvider)
                {
                    var result = imageProvider.GetImage(name);
                    if (result == null)
                    {
                        return null;
                    }

                    var bitMap = result as Bitmap;
                    if (bitMap == null)
                    {
                        bitMap = new Bitmap(result);
                    }

                    var imageSource = bitMap.ToImageSource();

                    ImageSourceHash.Add(name, imageSource);
                    return imageSource;
                }
            }
        }
#endif

        /// <summary>
        /// Gets icon by name.
        /// </summary>
        /// <param name="name">Icon name.</param>
        /// <returns>Icon object.</returns>
        public static Icon GetIcon(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            lock (((ICollection) IconHash).SyncRoot)
            {
                if (IconHash.TryGetValue(name, out Icon icon))
                {
                    return icon;
                }

                var imageProvider = GetImageProviderByPath(name);
                if (imageProvider == null)
                {
                    throw new ArgumentException(StringHelper.FormatMessage(CommonMessages.ImageProviderNotFound, name));
                }

                lock (imageProvider)
                {
                    var result = imageProvider.GetIcon(name);
                    if (result == null)
                    {
                        return null;
                    }

                    IconHash.Add(name, result);
                    return result;
                }
            }
        }
    }
}