using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CommonUtils.Helpers;

namespace CommonUtils.CryptoProviders
{
    /// Static class for decoding and encoding the data.
    /// May be used with a different implementations of the <see cref="ICryptoProvider"/>.
    /// The instance of the <see cref="ICryptoProvider"/> is define by type of the <see cref="CryptoType"/>.
    public static class CryptoProvider
    {
        /// <summary>
        /// Simple IoC container for <see cref="ICryptoProvider"/> instances.
        /// </summary>
        private static readonly Dictionary<string, ICryptoProvider> CryptoProviderHash = new Dictionary<string, ICryptoProvider>();


        /// <summary>
        /// Static constructor.
        /// Sets two default crypto common providers:
        /// with the<see cref="CryptoType.None"/> type and with the <see cref="CryptoType.SHA256"/> type.
        /// </summary>
        static CryptoProvider()
        {
            SetCryptoProvider(CryptoType.None, new FakeCryptoProvider());
            SetCryptoProvider(CryptoType.SHA256, new SHA256CryproProvider());
        }

        /// <summary>
        /// Gets crypto provider by crypto type.
        /// </summary>
        /// <param name="cryptoType">The specified crypto type.</param>
        /// <returns>Implementation of <see cref="ICryptoProvider"/>.</returns>
        public static ICryptoProvider GetCryptoProvider(CryptoType cryptoType)
        {
            return GetCryptoProvider(UriHelper.EnumToPrefix(cryptoType));
        }

        /// <summary>
        /// Gets crypto provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="ICryptoProvider"/>.</returns>
        public static ICryptoProvider GetCryptoProvider(string prefix)
        {
            lock (((IDictionary)CryptoProviderHash).SyncRoot)
            {
                if (CryptoProviderHash.ContainsKey(prefix))
                    return CryptoProviderHash[prefix];

                return null;
            }
        }

        /// <summary>
        /// Sets crypto provider by crypto type.
        /// </summary>
        /// <param name="cryptoType">The specified crypto type.</param>
        /// <param name="provider">Implementation of <see cref="ICryptoProvider"/>.</param>
        public static void SetCryptoProvider(CryptoType cryptoType, ICryptoProvider provider)
        {
            SetCryptoProvider(UriHelper.EnumToPrefix(cryptoType), provider);
        }

        /// <summary>
        /// Sets crypto provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <param name="provider">Implementation of <see cref="ICryptoProvider"/>.</param>
        public static void SetCryptoProvider(string prefix, ICryptoProvider provider)
        {
            lock (((IDictionary)CryptoProviderHash).SyncRoot)
            {
                if (provider == null)
                {
                    RemoveCryptoProvider(prefix);
                }
                else
                {
                    CryptoProviderHash[prefix] = provider;
                }
            }
        }

        /// <summary>
        /// Removes crypto provider with the specified crypto type.
        /// </summary>
        /// <param name="cryptoType">The specified crypto type.</param>
        public static void RemoveCryptoProvider(CryptoType cryptoType)
        {
            RemoveCryptoProvider(UriHelper.EnumToPrefix(cryptoType));
        }

        /// <summary>
        /// Removes crypto provider with the specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        public static void RemoveCryptoProvider(string prefix)
        {
            lock (((IDictionary)CryptoProviderHash).SyncRoot)
            {
                CryptoProviderHash.Remove(prefix);
            }
        }

        /// <summary>
        /// Decodes data.
        /// </summary>
        /// <param name="cryptoType">Specified type of crypto provider.</param>
        /// <param name="stream">The encoded stream.</param>
        /// <returns>The decoded stream.</returns>
        public static Stream DecodeData(CryptoType cryptoType, Stream stream)
        {
            return DecodeData(UriHelper.EnumToPrefix(cryptoType), stream);
        }

        /// <summary>
        /// Decodes data.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of crypto provider.</param>
        /// <param name="stream">The encoded stream.</param>
        /// <returns>The decoded stream.</returns>
        public static Stream DecodeData(string prefixProvider, Stream stream)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var cryptoProvider = CheckCryptoProviderByPath(prefixProvider);

            return cryptoProvider.DecodeData(stream);
        }

        /// <summary>
        /// Encodes data.
        /// </summary>
        /// <param name="cryptoType">Specified type of crypto provider.</param>
        /// <param name="stream">The origin stream.</param>
        /// <returns>The encoded stream.</returns>
        public static Stream EncodeData(CryptoType cryptoType, Stream stream)
        {
            return EncodeData(UriHelper.EnumToPrefix(cryptoType), stream);
        }

        /// <summary>
        /// Encodes data.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of crypto provider.</param>
        /// <param name="stream">The origin stream.</param>
        /// <returns>The encoded stream.</returns>
        public static Stream EncodeData(string prefixProvider, Stream stream)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var cryptoProvider = CheckCryptoProviderByPath(prefixProvider);

            return cryptoProvider.EncodeData(stream);
        }

        /// <summary>
        /// Encodes string.
        /// </summary>
        /// <param name="cryptoType">Specified type of crypto provider.</param>
        /// <param name="string">The origin string.</param>
        /// <returns>The encoded string.</returns>
        public static String EncodeString(CryptoType cryptoType, String @string)
        {
            return EncodeString(UriHelper.EnumToPrefix(cryptoType), @string);
        }

        /// <summary>
        /// Encodes string.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of crypto provider.</param>
        /// <param name="string">The origin string.</param>
        /// <returns>The encoded string.</returns>
        public static String EncodeString(string prefixProvider, String @string)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var cryptoProvider = CheckCryptoProviderByPath(prefixProvider);

            return cryptoProvider.EncodeString(@string);
        }

        /// <summary>
        /// Decodes string.
        /// </summary>
        /// <param name="cryptoType">Specified type of crypto provider.</param>
        /// <param name="string">The encoded string.</param>
        /// <returns>The decoded string.</returns>
        public static String DecodeString(CryptoType cryptoType, String @string)
        {
            return DecodeString(UriHelper.EnumToPrefix(cryptoType), @string);
        }

        /// <summary>
        /// Decodes string.
        /// </summary>
        /// <param name="prefixProvider">Specified URI prefix of crypto provider.</param>
        /// <param name="string">The encoded string.</param>
        /// <returns>The decoded string.</returns>
        public static String DecodeString(string prefixProvider, String @string)
        {
            if (prefixProvider == null)
            {
                throw new ArgumentNullException(nameof(prefixProvider));
            }

            var cryptoProvider = CheckCryptoProviderByPath(prefixProvider);

            return cryptoProvider.DecodeString(@string);
        }

        /// <summary>
        /// Gets crypto provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="ICryptoProvider"/>.</returns>
        private static ICryptoProvider CheckCryptoProviderByPath(string path)
        {
            var cryptoProvider = GetCryptoProviderByPath(path);
            if (cryptoProvider == null)
            {
                throw new ArgumentException(String.Format(CommonMessages.CryptoProviderNotFound, path));
            }

            return cryptoProvider;
        }

        /// <summary>
        /// Gets crypto provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="ICryptoProvider"/>.</returns>
        private static ICryptoProvider GetCryptoProviderByPath(string path)
        {
            return GetCryptoProvider(UriHelper.GetPrefix(path));
        }
    }
}