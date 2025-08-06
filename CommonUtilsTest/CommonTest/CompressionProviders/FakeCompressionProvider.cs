using System;
using System.IO;

namespace CommonUtils.CompressionProviders
{
    /// <summary>
    /// Fake compression provider.
    /// Don't compressed data.
    /// </summary>
    public class FakeCompressionProvider : ICompressionProvider
    {
        /// <summary>
        /// Compressing data.
        /// </summary>
        /// <param name="stream">The origin stream.</param>
        /// <returns>The compressed stream.</returns>
        public Stream CompressData(Stream stream)
        {
            if (stream == null)
                return null;

            var output = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(output);

            output.Position = 0;
            return output;
        }

        /// <summary>
        /// Decompressing data.
        /// </summary>
        /// <param name="stream">The compressed stream.</param>
        /// <returns>The decompressed stream.</returns>
        public Stream DecompressData(Stream stream)
        {
            if (stream == null)
                return null;

            var output = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(output);

            output.Position = 0;
            return output;
        }

        /// <summary>
        /// Compressing string.
        /// </summary>
        /// <param name="string">The origin string.</param>
        /// <returns>The compressed string.</returns>
        public String CompressString(String @string)
        {
            return @string;
        }

        /// <summary>
        /// Decompressing string.
        /// </summary>
        /// <param name="string">The compressed string.</param>
        /// <returns>The decompressed string.</returns>
        public String DecompressString(String @string)
        {
            return @string;
        }
    }
}