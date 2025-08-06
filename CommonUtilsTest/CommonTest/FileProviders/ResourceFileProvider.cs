using System;
using System.IO;
using System.Reflection;
using CommonUtils.Helpers;

namespace CommonUtils.FileProviders
{
    /// <summary>
    /// Class for working with the assembly recource files.
    /// </summary>
    public class ResourceFileProvider : IFileProvider
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResourceFileProvider()
        {
        }

        /// <summary>
        /// Constructor with the assembly.
        /// </summary>
        /// <param name="resourceAssembly">Specified assembly.</param>
        public ResourceFileProvider(Assembly resourceAssembly) : this(resourceAssembly, String.Empty)
        {
        }

        /// <summary>
        /// Constructor with the assembly and the root resource path.
        /// </summary>
        /// <param name="resourceAssembly">Specified assembly.</param>
        /// <param name="rootPath">Specified root resource path.</param>
        public ResourceFileProvider(Assembly resourceAssembly, string rootPath)
        {
            if (resourceAssembly == null)
            {
                throw new ArgumentNullException(nameof(resourceAssembly));
            }
            if (rootPath == null)
            {
                throw new ArgumentNullException(nameof(rootPath));
            }

            ResourceAssembly = resourceAssembly;
            RootPath = rootPath.Replace('.', UriHelper.PathSeparator);
        }

        /// <summary>
        /// Gets the resource assembly.
        /// </summary>
        public Assembly ResourceAssembly { get; } = null;

        /// <summary>
        /// Gets the root resource path.
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

            var realFileName = UriHelper.GetTail(fileName);

            var assembly = GetResourceAssembly(realFileName);

            var stream = assembly?.GetManifestResourceStream(GetFullPath(realFileName));
            return stream != null;
        }

        /// <summary>
        /// Clears the file content.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public void ClearFile(String fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public void DeleteFile(String fileName)
        {
            throw new NotImplementedException();
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

            var realFileName = UriHelper.GetTail(fileName);

            var assembly = GetResourceAssembly(realFileName);

            return assembly?.GetManifestResourceStream(GetFullPath(realFileName));
        }

        /// <summary>
        /// Opens the file for writing.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        public Stream WriteFile(String fileName)
        {
            throw new NotImplementedException();
        }


        private Assembly GetResourceAssembly(string path)
        {
            var assembly = ResourceAssembly;
            if (assembly == null)
            {
                string assemblyName = UriHelper.GetFirst(path);
                assembly = ClassFactory.GetAssembly(assemblyName);
            }

            return assembly;
        }

        private string GetFullPath(string path)
        {
            var fullPath = UriHelper.GetTail(path);

            if (ResourceAssembly == null)
            {
                string assemblyName = UriHelper.GetFirst(fullPath);
                if (assemblyName.Length > 0)
                {
                    fullPath = fullPath.Substring(assemblyName.Length + 1);
                }
            }

            if (RootPath.Length > 0)
            {
                fullPath = RootPath + "." + fullPath;
            }

            return fullPath.Replace(UriHelper.PathSeparator, '.');
        }
    }
}