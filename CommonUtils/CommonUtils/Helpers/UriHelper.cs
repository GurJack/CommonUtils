//using System;

//namespace CommonUtils.Helpers
//{
//    /// <summary>
//    /// Static class for working with URI string.
//    /// </summary>
//    public static class UriHelper
//    {
//        /// <summary>
//        /// The URI prefix separator string: "://".
//        /// </summary>

//        public const string PrefixSeparator = "://";
        
//        /// <summary>
//        /// The path separator char: "/".
//        /// </summary>
//        public const char PathSeparator = '/';

//        /// <summary>
//        /// Gets the first part of path before "/" separator.
//        /// </summary>
//        /// <param name="path">Full URI string.</param>
//        /// <returns>The first part of path without "/" separator.</returns>
//        public static string GetFirst(string path)
//        {
//            if (path == null)
//            {
//                throw new ArgumentNullException(nameof(path));
//            }

//            path = GetTail(path);
//            if (path.Length == 0)
//            {
//                return String.Empty;
//            }

//            int index = path.IndexOf(PathSeparator);
//            if (index < 0)
//            {
//                return path;
//            }

//            return path.Substring(0, index);
//        }

//        /// <summary>
//        /// Gets URI prefix of specified string.
//        /// Prefix is defined by "://" separator.
//        /// </summary>
//        /// <param name="path">Full URI string.</param>
//        /// <returns>URI prefix with "://" separator.</returns>
//        public static string GetPrefix(string path)
//        {
//            if (path == null)
//            {
//                throw new ArgumentNullException(nameof(path));
//            }

//            int index = path.IndexOf(PrefixSeparator);
//            if (index < 0)
//            {
//                return String.Empty;
//            }

//            return path.Substring(0, index + 3);
//        }

//        /// <summary>
//        /// Gets URI tail without prefix of specified string.
//        /// Prefix is defined by "://" separator.
//        /// </summary>
//        /// <param name="path">Full URI string.</param>
//        /// <returns>URI prefix without prefix and "://" separator.</returns>
//        public static string GetTail(string path)
//        {
//            if (path == null)
//            {
//                throw new ArgumentNullException(nameof(path));
//            }

//            int index = path.IndexOf(PrefixSeparator);
//            if (index < 0)
//            {
//                return path;
//            }

//            return path.Substring(index + 3);
//        }

//        /// <summary>
//        /// Combines two path strings.
//        /// </summary>
//        /// <param name="startPath">The start path.</param>
//        /// <param name="finishPath">The finish path.</param>
//        /// <returns>A string containing the combined paths.
//        /// If one of the specified paths is a zero-length string, this method returns the other path.</returns>
//        public static string Combine(string startPath, string finishPath)
//        {
//            if (startPath == null)
//            {
//                throw new ArgumentNullException(nameof(startPath));
//            }
//            if (finishPath == null)
//            {
//                throw new ArgumentNullException(nameof(finishPath));
//            }

//            if (GetTail(startPath).Length == 0)
//            {
//                return startPath + finishPath;
//            }

//            if (GetTail(finishPath).Length == 0)
//            {
//                return startPath;
//            }

//            return startPath + PathSeparator + GetTail(finishPath);
//        }

//        /// <summary>
//        /// Gets URI prefix of specified enum element.
//        /// </summary>
//        /// <param name="enumElement">Specified enum element.</param>
//        /// <returns>URI prefix.</returns>
//        public static string EnumToPrefix(Enum enumElement)
//        {
//            return enumElement.ToString().ToLower() + PrefixSeparator;
//        }

//        /// <summary>
//        /// Gets URI prefix of specified object key.
//        /// </summary>
//        /// <param name="objectKey">Specified object key.</param>
//        /// <returns>URI prefix.</returns>
//        public static string ObjectToPrefix(IKey objectKey)
//        {
//            return objectKey.Key.ToString().ToLower() + PrefixSeparator;
//        }
//    }
//}