using System;
using System.IO;

namespace CommonUtils.CryptoProviders
{
    /// <summary>
    /// Interface for decoding and encoding any data.
    /// </summary>
    public interface ICryptoProvider
    {
        /// <summary>
        /// Decodes data.
        /// </summary>
        /// <param name="stream">The encoded stream.</param>
        /// <returns>The decoded stream.</returns>
        Stream DecodeData(Stream stream);

        /// <summary>
        /// Encodes data.
        /// </summary>
        /// <param name="stream">The origin stream.</param>
        /// <returns>The encoded stream.</returns>
        Stream EncodeData(Stream stream);

        /// <summary>
        /// Encodes string.
        /// </summary>
        /// <param name="string">The origin string.</param>
        /// <returns>The encoded string.</returns>
        String EncodeString(String @string);

        /// <summary>
        /// Decodes string.
        /// </summary>
        /// <param name="string">The encoded string.</param>
        /// <returns>The decoded string.</returns>
        String DecodeString(String @string);
    }
}