using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtils.Helpers;

namespace CommonUtils.FileProviders
{
    /// <summary>
    /// Static class for working with an abstract file objects.
    /// May be used with a different implementations of the <see cref="IFileProvider"/>.
    /// The instance of the <see cref="IFileProvider"/> is define by URI prefix in the file name.
    /// </summary>
    public static class FileProvider
    {
        /// <summary>
        /// The file sistem URI prefix.
        /// </summary>
        public const string File = "file://";

        /// <summary>
        /// The URI prefix for assembly resources.
        /// </summary>
        public const string Resource = "res://";

        /// <summary>
        /// Simple IoC container for <see cref="IFileProvider"/> instances.
        /// </summary>
        private static readonly Dictionary<string, IFileProvider> FileProviderHash = new Dictionary<string, IFileProvider>();

        /// <summary>
        /// Static constructor.
        /// Sets two default file common providers:
        /// with an empty prefix and with a "file://" prefix.
        /// </summary>
        static FileProvider()
        {
            SetFileProvider(String.Empty, new CommonFileProvider());
            SetFileProvider(File, new CommonFileProvider());
            SetFileProvider(Resource, new ResourceFileProvider());
        }


        /// <summary>
        /// Gets file provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="IFileProvider"/>.</returns>
        public static IFileProvider GetFileProvider(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((IDictionary)FileProviderHash).SyncRoot)
            {
                if (FileProviderHash.ContainsKey(prefix))
                    return FileProviderHash[prefix];

                return null;
            }
        }

        /// <summary>
        /// Sets file provider by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <param name="provider">Implementation of <see cref="IFileProvider"/>.</param>
        public static void SetFileProvider(string prefix, IFileProvider provider)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((IDictionary)FileProviderHash).SyncRoot)
            {
                if (provider == null)
                {
                    RemoveFileProvider(prefix);
                }
                else
                {
                    FileProviderHash[prefix] = provider;
                }
            }
        }

        /// <summary>
        /// Removes file provider with the specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="IFileProvider"/>.</returns>
        public static void RemoveFileProvider(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((IDictionary)FileProviderHash).SyncRoot)
            {
                FileProviderHash.Remove(prefix);
            }
        }

        /// <summary>
        /// Gets the all file providers prefix list.
        /// </summary>
        /// <returns>The prefix list for all file providers.</returns>
        public static string[] GetFileProviderPrefixList()
        {
            lock (((IDictionary)FileProviderHash).SyncRoot)
            {
                return FileProviderHash.Select(f => f.Key).ToArray();
            }
        }

        /// <summary>
        /// Checking the file existing.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <returns>True if file exists; otherwise, false.</returns>
        public static bool ExistsFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            return fileProvider.ExistsFile(fileName);
        }

        /// <summary>
        /// Opens the file for reading.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        public static Stream ReadFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            return fileProvider.ReadFile(fileName);
        }

        /// <summary>
        /// Opens the file for writing.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        public static Stream WriteFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            return fileProvider.WriteFile(fileName);
        }

        /// <summary>
        /// Gets the file content as a string.
        /// Default encoding is UTF-8.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <returns>File content.</returns>
        public static string ReadText(string fileName)
        {
            return ReadText(fileName, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the file content as a string.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="encoding">Specified encoding.</param>
        /// <returns>File content.</returns>
        public static string ReadText(string fileName, Encoding encoding)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            using (var stream = fileProvider.ReadFile(fileName))
            {
                return StreamHelper.ReadText(stream, encoding);
            }
        }

        /// <summary>
        /// Gets the file content as byte array.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <returns>File content.</returns>
        public static byte[] ReadBytes(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            using (var stream = fileProvider.ReadFile(fileName))
            {
                return StreamHelper.ReadBytes(stream);
            }
        }

        /// <summary>
        /// Appends the byte array to the file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="content">Specified byte array.</param>
        public static void AppendBytes(string fileName, byte[] content)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            using (var stream = fileProvider.WriteFile(fileName))
            {
                stream.Position = stream.Length;
                StreamHelper.WriteBytes(stream, content);
            }
        }

        /// <summary>
        /// Writes the byte array to the file.
        /// Clears the file before writing.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="content">Specified byte array.</param>
        public static void WriteBytes(string fileName, byte[] content)
        {
            ClearFile(fileName);
            AppendBytes(fileName, content);
        }

        /// <summary>
        /// Appends the string to the file.
        /// Default encoding is UTF-8.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="content">Specified string.</param>
        public static void AppendText(string fileName, string content)
        {
            AppendText(fileName, content, Encoding.UTF8);
        }

        /// <summary>
        /// Appends the string to the file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="content">Specified string.</param>
        /// <param name="encoding">Specified encoding.</param>
        public static void AppendText(string fileName, string content, Encoding encoding)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            using (var stream = fileProvider.WriteFile(fileName))
            {
                stream.Position = stream.Length;
                StreamHelper.WriteText(stream, content, encoding);
            }
        }

        /// <summary>
        /// Writes the string to the file.
        /// Clears the file before writing.
        /// Default encoding is UTF-8.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="content">Specified string.</param>
        public static void WriteText(string fileName, string content)
        {
            WriteText(fileName, content, Encoding.UTF8);
        }

        /// <summary>
        /// Writes the string to the file.
        /// Clears the file before writing.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="content">Specified string.</param>
        /// <param name="encoding">Specified encoding.</param>
        public static void WriteText(string fileName, string content, Encoding encoding)
        {
            ClearFile(fileName);
            AppendText(fileName, content);
        }


        /// <summary>
        /// Creates the new empty file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public static void CreateFile(string fileName)
        {
            var folder = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            WriteBytes(fileName, new byte[0]);
        }

        /// <summary>
        /// Clears the file content.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public static void ClearFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            fileProvider.ClearFile(fileName);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public static void DeleteFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileProvider = CheckFileProviderByPath(fileName);

            fileProvider.DeleteFile(fileName);
        }


        /// <summary>
        /// Gets file provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="IFileProvider"/>.</returns>
        private static IFileProvider CheckFileProviderByPath(string path)
        {
            var fileProvider = GetFileProviderByPath(path);
            if (fileProvider == null)
            {
                throw new ArgumentException(String.Format(CommonMessages.FileProviderNotFound, path));
            }

            return fileProvider;
        }

        /// <summary>
        /// Gets file provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="IFileProvider"/>.</returns>
        private static IFileProvider GetFileProviderByPath(string path)
        {
            return GetFileProvider(UriHelper.GetPrefix(path));
        }
    }
}