using System.IO;

namespace CommonUtils.FileProviders
{
    /// <summary>
    /// Interface for working with an abstract file objects.
    /// </summary>
    public interface IFileProvider
    {
        /// <summary>
        /// Checking the file existing.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <returns>True if file exists; otherwise, false.</returns>
        bool ExistsFile(string fileName);

        /// <summary>
        /// Clears the file content.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        void ClearFile(string fileName);
        
        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        void DeleteFile(string fileName);

        /// <summary>
        /// Opens the file for reading.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        Stream ReadFile(string fileName);
        
        /// <summary>
        /// Opens the file for writing.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File stream.</returns>
        Stream WriteFile(string fileName);
    }
}