using System;
using System.IO;
using CommonUtils.Helpers;

namespace CommonUtils.FileProviders
{
    /// <summary>
    /// Class for working with the file system objects.
    /// </summary>
    public class CommonFileProvider : IFileProvider
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommonFileProvider()
		{
        }

        /// <summary>
        /// Constructor with a root directory name.
        /// </summary>
        /// <param name="rootPath">The root directory for files.</param>
        public CommonFileProvider(string rootPath)
			: this()
		{
		    RootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
        }


        /// <summary>
        /// Gets the root directory name.
        /// </summary>
        public string RootPath { get; } = String.Empty;

        /// <summary>
        /// Checking the file existing.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <returns>True if file exists; otherwise, false.</returns>
        public bool ExistsFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return File.Exists(GetRealPath(fileName));
        }

        /// <summary>
        /// Clears the file content.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public void ClearFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!ExistsFile(fileName))
            {
                return;
            }

            var realFileName = GetRealPath(fileName);

            using (File.Open(realFileName, FileMode.Truncate))
            {
            }
        }

        /// <summary>
        /// Opens the file for reading.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        public Stream ReadFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!ExistsFile(fileName))
            {
                return null;
            }

            var realFileName = GetRealPath(fileName);

            return File.Open(realFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public void DeleteFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!ExistsFile(fileName))
            {
                return;
            }

            var realFileName = GetRealPath(fileName);

            try
            {
                File.Delete(realFileName);
            }
            catch (UnauthorizedAccessException)
            {
                RemoveReadOnlyFlag(realFileName);
                File.Delete(realFileName);
            }
        }

        /// <summary>
        /// Opens the file for writing.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        public Stream WriteFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var realFileName = GetRealPath(fileName);

            return File.Open(realFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        }

        private string GetRealPath(string path)
        {
            return Path.Combine(RootPath, UriHelper.GetTail(path)).Replace(UriHelper.PathSeparator, Path.DirectorySeparatorChar);
        }

        private void RemoveReadOnlyFlag(string path)
        {
            var attributes = File.GetAttributes(path);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(path, attributes ^ FileAttributes.ReadOnly);
            }

            if (Directory.Exists(path))
            {
                var entries = Directory.GetFileSystemEntries(path);
                foreach (var entry in entries)
                {
                    RemoveReadOnlyFlag(entry);
                }
            }
        }
    }
}