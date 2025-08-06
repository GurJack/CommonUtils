using System;

namespace CommonUtils.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a unhandled error occurs only.
    /// </summary>
    [Serializable]
    public class UnhandledException : Exception
    {
        /// <summary>
        /// Constructor with the inner exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public UnhandledException(Exception innerException) :
            base(null, innerException)
        {
        }
    }
}