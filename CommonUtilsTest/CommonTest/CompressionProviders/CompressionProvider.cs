using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CommonUtils.Helpers;

namespace CommonUtils.CompressionProviders
{
    /// <summary>
    /// Static class for compressing and decompressing the data.
    /// May be used with a different implementations of the <see cref="ICompressionProvider"/>.
    /// The instance of the <see cref="ICompressionProvider"/> is define by type of the <see cref="CompressionType"/>.
    /// </summary>
    public static class CompressionProvider
    {
        /// <summary>
        /// Simple IoC container for <see cref="ICompressionProvider"/> instances.
        /// </summary>
        private static readonly Dictionary<string, ICompressionProvider> CompressionProviderHash = new Dictionary<string, ICompressionProvider>();

        /// <summary>
        /// Static constructor.
        /// Sets two default compression common providers:
        /// with the<see cref="CompressionType.None"/> type and with the <see cref="CompressionType.GZip"/> type.
        /// </summary>
        static CompressionProvider()
        {
            SetCompressionProvider(CompressionType.None, new FakeCompressionProvider());
            SetCompressionProvider(CompressionType.GZip, new GZipCompressionProvider());
        }

        /// <summary>
        /// Gets compression provider by compression type.
        /// </summary>
        /// <param name="compressionType">The specified compression type.</param>
        /// <returns>Implementation of <see cref="ICompressionProvider"/>.</returns>
        public static ICompressionProvider GetCompressionProvider(CompressionType compressionType)
        {
            return GetCompressionProvider(UriHelper.EnumToPrefix(compressionType));
        }

        /// <summary>
        /// Gets compression provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="ICompressionProvider"/>.</returns>
        public static ICompressionProvider GetCompressionProvider(string prefix)
        {
            lock (((IDictionary) CompressionProviderHash).SyncRoot)
            {
                if (CompressionProviderHash.ContainsKey(prefix))
                    return CompressionProviderHash[prefix];

                return null;
            }
        }

        /// <summary>
        /// Sets compression provider by compression type.
        /// </summary>
        /// <param name="compressionType">The specified compression type.</param>
        /// <param name="provider">Implementation of <see cref="ICompressionProvider"/>.</param>
        public static void SetCompressionProvider(CompressionType compressionType, ICompressionProvider provider)
        {
            SetCompressionProvider(UriHelper.EnumToPrefix(compressionType), provider);
        }

        /// <summary>
        /// Sets compression provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <param name="provider">Implementation of <see cref="ICompressionProvider"/>.</param>
        public static void SetCompressionProvider(string prefix, ICompressionProvider provider)
        {
            lock (((IDictionary)CompressionProviderHash).SyncRoot)
            {
                if (provider == null)
                {
                    RemoveCompressionProvider(prefix);
                }
                else
                {
                    CompressionProviderHash[prefix] = provider;
                }
            }
        }

        /// <summary>
        /// Removes Compression provider with the specified compression type.
        /// </summary>
        /// <param name="compressionType">The specified compression type.</param>
        public static void RemoveCompressionProvider(CompressionType compressionType)
        {
            RemoveCompressionProvider(UriHelper.EnumToPrefix(compressionType));
        }

        /// <summary>
        /// Removes Compression provider with the specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        public static void RemoveCompressionProvider(string prefix)
        {
            lock (((IDictionary)CompressionProviderHash).SyncRoot)
            {
                CompressionProviderHash.Remove(prefix);
            }
        }

        /// <summary>
        /// Compressing data.
        /// </summary>
        /// <param name="compressionType">Specified type of compression provider.</param>
        /// <param name="stream">The origin stream.</param>
        /// <returns>The compressed stream.</returns>
        public static Stream CompressData(CompressionType compressionType, Stream stream)
        {
            return CompressData(UriHelper.EnumToPrefix(compressionType), stream);
        }

        /// <summary>
        /// Compressing data.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of compression provider.</param>
        /// <param name="stream">The origin stream.</param>
        /// <returns>The compressed stream.</returns>
        public static Stream CompressData(string prefixProvider, Stream stream)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var compressionProvider = CheckCompressionProviderByPath(prefixProvider);

            return compressionProvider.CompressData(stream);
        }

        /// <summary>
        /// Decompressing data.
        /// </summary>
        /// <param name="compressionType">Specified type of compression provider.</param>
        /// <param name="stream">The compressed stream.</param>
        /// <returns>The decompressed stream.</returns>
        public static Stream DecompressData(CompressionType compressionType, Stream stream)
        {
            return DecompressData(UriHelper.EnumToPrefix(compressionType), stream);
        }

        /// <summary>
        /// Decompressing data.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of compression provider.</param>
        /// <param name="stream">The compressed stream.</param>
        /// <returns>The decompressed stream.</returns>
        public static Stream DecompressData(string prefixProvider, Stream stream)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var compressionProvider = CheckCompressionProviderByPath(prefixProvider);

            return compressionProvider.DecompressData(stream);
        }

        /// <summary>
        /// Compressing string.
        /// </summary>
        /// <param name="compressionType">Specified type of compression provider.</param>
        /// <param name="string">The origin string.</param>
        /// <returns>The compressed string.</returns>
        public static String CompressString(CompressionType compressionType, String @string)
        {
            return CompressString(UriHelper.EnumToPrefix(compressionType), @string);
        }

        /// <summary>
        /// Compressing string.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of compression provider.</param>
        /// <param name="string">The origin string.</param>
        /// <returns>The compressed string.</returns>
        public static String CompressString(string prefixProvider, String @string)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var compressionProvider = CheckCompressionProviderByPath(prefixProvider);

            return compressionProvider.CompressString(@string);
        }

        /// <summary>
        /// Decompressing string.
        /// </summary>
        /// <param name="compressionType">Specified type of compression provider.</param>
        /// <param name="string">The compressed string.</param>
        /// <returns>The decompressed string.</returns>
        public static String DecompressString(CompressionType compressionType, String @string)
        {
            return DecompressString(UriHelper.EnumToPrefix(compressionType), @string);
        }

        /// <summary>
        /// Decompressing string.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of compression provider.</param>
        /// <param name="string">The compressed string.</param>
        /// <returns>The decompressed string.</returns>
        public static String DecompressString(string prefixProvider, String @string)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var compressionProvider = CheckCompressionProviderByPath(prefixProvider);

            return compressionProvider.DecompressString(@string);
        }

        /// <summary>
        /// Gets compression provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="ICompressionProvider"/>.</returns>
        private static ICompressionProvider CheckCompressionProviderByPath(string path)
        {
            var compressionProvider = GetCompressionProviderByPath(path);
            if (compressionProvider == null)
            {
                throw new ArgumentException(String.Format(CommonMessages.CompressionProviderNotFound, path));
            }

            return compressionProvider;
        }

        /// <summary>
        /// Gets compression provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="ICompressionProvider"/>.</returns>
        private static ICompressionProvider GetCompressionProviderByPath(string path)
        {
            return GetCompressionProvider(UriHelper.GetPrefix(path));
        }
    }
}