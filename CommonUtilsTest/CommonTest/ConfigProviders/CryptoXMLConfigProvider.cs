using System;
using System.IO;
using CommonUtils.CompressionProviders;
using CommonUtils.CryptoProviders;
using CommonUtils.FileProviders;

namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// Class for getting and setting custom configuration data with encoding/decoding, compressing/decompressing.
    /// Loads the file on constructor and saves the file on Save method.
    /// </summary>
    public class CryptoXMLConfigProvider : XmlConfigProvider
    {
        private readonly ICryptoProvider _cryptoProvider;
        private readonly ICompressionProvider _compressionProvider;

        /// <summary>
        /// Constructor with configuration file name.
        /// Crypto and compression providers is None.
        /// Loads the configuration file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public CryptoXMLConfigProvider(string fileName) : this(fileName, CryptoProvider.GetCryptoProvider(CryptoType.None), CompressionProvider.GetCompressionProvider(CompressionType.None))
        {
        }

        /// <summary>
        /// Constructor with configuration file name and crypro provider.
        /// Compression provider is None.
        /// Loads the configuration file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="cryptoProvider">Specified crypro provider.</param>
        public CryptoXMLConfigProvider(string fileName, ICryptoProvider cryptoProvider) : this(fileName, cryptoProvider, CompressionProvider.GetCompressionProvider(CompressionType.None))
        {
        }

        /// <summary>
        /// Constructor with configuration file name and compression provider.
        /// Crypto provider is None.
        /// Loads the configuration file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="compressionProvider">Specified compression provider.</param>
        public CryptoXMLConfigProvider(string fileName, ICompressionProvider compressionProvider) : this(fileName, CryptoProvider.GetCryptoProvider(CryptoType.None), compressionProvider)
        {
        }

        /// <summary>
        /// Constructor with configuration file name, crypro provider and compression provider.
        /// Loads the configuration file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="cryptoProvider">Specified crypro provider.</param>
        /// <param name="compressionProvider">Specified compression provider.</param>
        public CryptoXMLConfigProvider(string fileName, ICryptoProvider cryptoProvider, ICompressionProvider compressionProvider) : base(fileName)
        {
            if (cryptoProvider == null)
                throw new ArgumentNullException(nameof(cryptoProvider));

            if (compressionProvider == null)
                throw new ArgumentNullException(nameof(compressionProvider));

            _cryptoProvider = cryptoProvider;
            _compressionProvider = compressionProvider;

            Load();
        }

        /// <summary>
        /// Gets the available to read flag.
        /// </summary>
        protected override Boolean IsAvailableRead => base.IsAvailableRead &&
                                                      IsProvidersInitialized;

        /// <summary>
        /// Gets the providers initialized flag.
        /// </summary>
        protected Boolean IsProvidersInitialized => _cryptoProvider != null &&
                                                    _compressionProvider != null;

        /// <summary>
        /// Reads the configuration data.
        /// </summary>
        /// <returns></returns>
        protected override Stream ReadData()
        {
            using (var fileStream = base.ReadData())
            {
                return ProcessReadStream(fileStream);
            }
        }

        /// <summary>
        /// Decodes and decompresses stream.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        protected Stream ProcessReadStream(Stream fileStream)
        {
            using (var decoded = _cryptoProvider.DecodeData(fileStream))
            {
                var decompressed = _compressionProvider.DecompressData(decoded);

                return decompressed;
            }
        }

        /// <summary>
        /// Writes the configuration data.
        /// </summary>
        protected override void WriteData()
        {
            using (var data = new MemoryStream())
            {
                ConfigTo(data);

                using (var processedStream = ProcessWriteStream(data))
                {
                    using (var stream = FileProvider.WriteFile(FileName))
                    {
                        processedStream.WriteTo(stream);
                    }
                }
            }
        }

        /// <summary>
        /// Compresses and encodes stream.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        protected MemoryStream ProcessWriteStream(Stream fileStream)
        {
            using (var compressed = _compressionProvider.CompressData(fileStream))
            {
                using (var encoded = _cryptoProvider.EncodeData(compressed))
                {
                    var output = new MemoryStream();
                    encoded.CopyTo(output);

                    return output;
                }
            }
        }
    }
}