using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommonUtils.Compress
{
    /// <summary>
    /// Class for compressing and decompressing any data.
    /// Algorithm is GZip.
    /// </summary>
    public class GZipCompressionProvider : ICompressionProvider
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
            
            stream.Position = 0;
            var compressedStream = new MemoryStream();
            using (var csStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
            {
                var buff = new byte[stream.Length];
                stream.Read(buff, 0, buff.Length);

                csStream.Write(buff, 0, buff.Length);
                csStream.Close();

                compressedStream.Position = 0;
                return compressedStream;
            }
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

            stream.Position = 0;
            using (var mStream = new MemoryStream())
            {
                stream.CopyTo(mStream);
                mStream.Position = 0;

                using (var csStream = new GZipStream(mStream, CompressionMode.Decompress))
                {
                    var decompressedStream = new MemoryStream();

                    var buffer = new byte[1024];
                    int nRead;
                    while ((nRead = csStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        decompressedStream.Write(buffer, 0, nRead);
                    }

                    decompressedStream.Position = 0;
                    return decompressedStream;
                }
            }
        }



        /// <summary>
        /// Compressing string.
        /// </summary>
        /// <param name="string">The origin string.</param>
        /// <returns>The compressed string.</returns>
        public String CompressString(String @string)
        {
            if (@string == null)
                return null;

            var byteArray = Encoding.UTF8.GetBytes(@string);

            using (var compressedStream = new MemoryStream())
            {
                using (var csStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
                {
                    csStream.Write(byteArray, 0, byteArray.Length);
                    csStream.Close();

                    byteArray = compressedStream.ToArray();

                    return Convert.ToBase64String(byteArray);
                }
            }
        }

        /// <summary>
        /// Decompressing string.
        /// </summary>
        /// <param name="string">The compressed string.</param>
        /// <returns>The decompressed string.</returns>
        public String DecompressString(String @string)
        {
            if (@string == null)
                return null;

            var byteArray = Convert.FromBase64String(@string);
            using (var stream = new MemoryStream(byteArray))
            {
                using (var csStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    var decompressedStream = new MemoryStream();

                    var buffer = new byte[4096];
                    int nRead;
                    while ((nRead = csStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        decompressedStream.Write(buffer, 0, nRead);
                    }

                    return Encoding.UTF8.GetString(decompressedStream.ToArray());
                }
            }
        }
    }
}
