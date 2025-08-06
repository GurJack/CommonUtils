//#if NET461 || NET47 || NET471 || NET472
//using System;
//using Microsoft.Win32;

//namespace CommonUtils.ConfigProviders
//{
//    /// <summary>
//    /// Class for getting and setting custom configuration data with encoding/decoding, compressing/decompressing.
//    /// Uses the Windows Registry.
//    /// Default root registry key is HKEY_CURRENT_USER.
//    /// Default root path is Software/CompanyName/ProductName.
//    /// </summary>
//    public class CryptoRegistryConfigProvider : RegistryConfigProvider
//    {
//        private readonly ICryptoProvider _cryptoProvider;
//        private readonly ICompressionProvider _compressionProvider;


//        /// <summary>
//        /// Default constructor.
//        /// </summary>
//        public CryptoRegistryConfigProvider() : this(DefaultRoot)
//        {
//        }

//        /// <summary>
//        /// Constructor with  a root registry key.
//        /// Crypto and compression providers is None.
//        /// </summary>
//        /// <param name="rootKey">Specified file name.</param>
//        public CryptoRegistryConfigProvider(RegistryKey rootKey) : this(rootKey, CryptoProvider.GetCryptoProvider(CryptoType.None), CompressionProvider.GetCompressionProvider(CompressionType.None))
//        {
//        }

//        /// <summary>
//        /// Constructor with  a root registry key and crypro provider.
//        /// Compression provider is None.
//        /// </summary>
//        /// <param name="rootKey">Specified file name.</param>
//        /// <param name="cryptoProvider">Specified crypro provider.</param>
//        public CryptoRegistryConfigProvider(RegistryKey rootKey, ICryptoProvider cryptoProvider) : this(rootKey, cryptoProvider, CompressionProvider.GetCompressionProvider(CompressionType.None))
//        {
//        }

//        /// <summary>
//        /// Constructor with crypro provider.
//        /// Compression provider is None.
//        /// </summary>
//        /// <param name="cryptoProvider">Specified crypro provider.</param>
//        public CryptoRegistryConfigProvider(ICryptoProvider cryptoProvider) : this(DefaultRoot, cryptoProvider, CompressionProvider.GetCompressionProvider(CompressionType.None))
//        {
//        }

//        /// <summary>
//        /// Constructor with a root registry key and compression provider.
//        /// Crypto provider is None.
//        /// </summary>
//        /// <param name="rootKey">Specified file name.</param>
//        /// <param name="compressionProvider">Specified compression provider.</param>
//        public CryptoRegistryConfigProvider(RegistryKey rootKey, ICompressionProvider compressionProvider) : this(rootKey, CryptoProvider.GetCryptoProvider(CryptoType.None), compressionProvider)
//        {
//        }

//        /// <summary>
//        /// Constructor with compression provider.
//        /// Crypto provider is None.
//        /// </summary>
//        /// <param name="compressionProvider">Specified compression provider.</param>
//        public CryptoRegistryConfigProvider(ICompressionProvider compressionProvider) : this(DefaultRoot, CryptoProvider.GetCryptoProvider(CryptoType.None), compressionProvider)
//        {
//        }

//        /// <summary>
//        /// Constructor with a root registry key, crypro provider and compression provider.
//        /// </summary>
//        /// <param name="rootKey">Specified file name.</param>
//        /// <param name="cryptoProvider">Specified crypro provider.</param>
//        /// <param name="compressionProvider">Specified compression provider.</param>
//        public CryptoRegistryConfigProvider(RegistryKey rootKey, ICryptoProvider cryptoProvider, ICompressionProvider compressionProvider) : base(rootKey)
//        {
//            if (cryptoProvider == null)
//                throw new ArgumentNullException(nameof(cryptoProvider));

//            if (compressionProvider == null)
//                throw new ArgumentNullException(nameof(compressionProvider));

//            _cryptoProvider = cryptoProvider;
//            _compressionProvider = compressionProvider;
//        }

//        /// <summary>
//        /// Constructor with crypro provider and compression provider.
//        /// </summary>
//        /// <param name="cryptoProvider">Specified crypro provider.</param>
//        /// <param name="compressionProvider">Specified compression provider.</param>
//        public CryptoRegistryConfigProvider(ICryptoProvider cryptoProvider, ICompressionProvider compressionProvider) : base()
//        {
//            if (cryptoProvider == null)
//                throw new ArgumentNullException(nameof(cryptoProvider));

//            if (compressionProvider == null)
//                throw new ArgumentNullException(nameof(compressionProvider));

//            _cryptoProvider = cryptoProvider;
//            _compressionProvider = compressionProvider;
//        }

//        /// <summary>
//        /// Gets value from registry.
//        /// </summary>
//        /// <param name="value">Raw value readed from registry.</param>
//        /// <returns>String from registry.</returns>
//        protected override String GetValueFromRegistry(String value)
//        {
//            var decodedValue = _cryptoProvider.DecodeString(value);
//            var decompressedValue = _compressionProvider.DecompressString(decodedValue);

//            return decompressedValue;
//        }

//        /// <summary>
//        /// Gets value for writing to registry.
//        /// </summary>
//        /// <param name="value">Raw value.</param>
//        /// /// <returns>String value for writing to registry.</returns>
//        protected override String GetValueToRegistry(String value)
//        {
//            var compressedValue = _compressionProvider.CompressString(value);
//            var encodedValue = _cryptoProvider.EncodeString(compressedValue);

//            return encodedValue;
//        }
//    }
//}
//#endif