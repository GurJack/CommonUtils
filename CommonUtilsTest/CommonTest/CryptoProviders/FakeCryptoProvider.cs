using System;
using System.IO;

namespace CommonUtils.CryptoProviders
{
    /// <summary>
    /// Fake crypto provider.
	/// Don't encrypted data.
    /// </summary>
    public class FakeCryptoProvider : ICryptoProvider
    {
        /// <summary>
        /// Decodes data.
        /// </summary>
        /// <param name="stream"></param>
        public Stream DecodeData(Stream stream)
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
        /// Encodes data.
        /// </summary>
        /// <param name="stream"></param>
        public Stream EncodeData(Stream stream)
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
        /// Encodes string.
        /// </summary>
        /// <param name="string">The origin string.</param>
        /// <returns>The encoded string.</returns>
        public String EncodeString(String @string)
        {
            return @string;
        }

        /// <summary>
        /// Decodes string.
        /// </summary>
        /// <param name="string">The encoded string.</param>
        /// <returns>The decoded string.</returns>
        public String DecodeString(String @string)
        {
            return @string;
        }
    }
}