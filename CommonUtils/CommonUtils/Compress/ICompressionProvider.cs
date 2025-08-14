//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CommonUtils.Compress
//{
//    /// <summary>
//    /// Interface for compressing and decompressing any data.
//    /// </summary>
//    public interface ICompressionProvider
//    {
//        /// <summary>
//        /// Compressing data.
//        /// </summary>
//        /// <param name="stream">The origin stream.</param>
//        /// <returns>The compressed stream.</returns>
//        Stream CompressData(Stream stream);

//        /// <summary>
//        /// Decompressing data.
//        /// </summary>
//        /// <param name="stream">The compressed stream.</param>
//        /// <returns>The decompressed stream.</returns>
//        Stream DecompressData(Stream stream);

//        /// <summary>
//        /// Compressing string.
//        /// </summary>
//        /// <param name="string">The origin string.</param>
//        /// <returns>The compressed string.</returns>
//        String CompressString(String @string);

//        /// <summary>
//        /// Decompressing string.
//        /// </summary>
//        /// <param name="string">The compressed string.</param>
//        /// <returns>The decompressed string.</returns>
//        String DecompressString(String @string);

//        //void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName);
//    }
//}
