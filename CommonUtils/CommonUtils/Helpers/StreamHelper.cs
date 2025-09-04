//using System;
//using System.IO;
//using System.Text;

//namespace CommonUtils.Helpers
//{
//    /// <summary>
//    /// Static class for stream data translation.
//    /// </summary>
//    public static class StreamHelper
//    {
//        private const int BufferSize = 1000;

//        /// <summary>
//        /// Translates the source data to the destination stream.
//        /// </summary>
//        /// <param name="source">The source stream.</param>
//        /// <param name="destination">The destination stream.</param>
//        public static void Translate(Stream source, Stream destination)
//        {
//            if (source == null)
//            {
//                throw new ArgumentNullException(nameof(source));
//            }
//            if (destination == null)
//            {
//                throw new ArgumentNullException(nameof(destination));
//            }

//            using (var reader = new BinaryReader(source))
//            {
//                var buffer = new byte[BufferSize];

//                while (buffer.Length == BufferSize)
//                {
//                    buffer = reader.ReadBytes(BufferSize);
//                    if (buffer.Length > 0)
//                    {
//                        destination.Write(buffer, 0, buffer.Length);
//                    }
//                }
//            }
//        }


//        /// <summary>
//        /// Reads the binary content from the stream.
//        /// </summary>
//        /// <param name="source">The source stream.</param>
//        /// <returns>The binary content of the stream.</returns>
//        public static byte[] ReadBytes(Stream source)
//        {
//            if (source == null)
//            {
//                return new byte[0];
//            }

//            using (var destination = new MemoryStream())
//            {
//                Translate(source, destination);
//                return destination.ToArray();
//            }
//        }

//        /// <summary>
//        /// Reads the text content from the stream.
//        /// </summary>
//        /// <param name="source">The source stream.</param>
//        /// <returns>The text content of the stream.</returns>
//        public static string ReadText(Stream source)
//        {
//            if (source == null)
//            {
//                return String.Empty;
//            }

//            return ReadText(source, Encoding.UTF8);
//        }

//        /// <summary>
//        /// Reads the text content from the stream.
//        /// </summary>
//        /// <param name="source">The source stream.</param>
//        /// <param name="encoding">The encoding.</param>
//        /// <returns>The text content of the stream.</returns>
//        public static string ReadText(Stream source, Encoding encoding)
//        {
//            if (source == null)
//            {
//                return String.Empty;
//            }

//            if (encoding == null)
//            {
//                encoding = Encoding.UTF8;
//            }

//            using (var reader = new StreamReader(source, encoding))
//            {
//                return reader.ReadToEnd();
//            }
//        }


//        /// <summary>
//        /// Writes the binary content into the stream.
//        /// </summary>
//        /// <param name="destination">The destination stream.</param>
//        /// <param name="content">The content.</param>
//        public static void WriteBytes(Stream destination, byte[] content)
//        {
//            if (destination == null)
//            {
//                throw new ArgumentNullException(nameof(destination));
//            }
//            if (content == null)
//            {
//                throw new ArgumentNullException(nameof(content));
//            }

//            destination.Write(content, 0, content.Length);
//        }

//        /// <summary>
//        /// Writes the text content into the stream.
//        /// </summary>
//        /// <param name="destination">The destination stream.</param>
//        /// <param name="text">The text content.</param>
//        public static void WriteText(Stream destination, string text)
//        {
//            WriteText(destination, text, Encoding.UTF8);
//        }

//        /// <summary>
//        /// Writes the text content into the stream.
//        /// </summary>
//        /// <param name="destination">The destination stream.</param>
//        /// <param name="text">The text content.</param>
//        /// <param name="encoding">The encoding.</param>
//        public static void WriteText(Stream destination, string text, Encoding encoding)
//        {
//            if (destination == null)
//            {
//                throw new ArgumentNullException(nameof(destination));
//            }
//            if (text == null)
//            {
//                throw new ArgumentNullException(nameof(text));
//            }

//            using (var writer = new StreamWriter(destination, encoding))
//            {
//                writer.Write(text);
//            }
//        }
//    }
//}