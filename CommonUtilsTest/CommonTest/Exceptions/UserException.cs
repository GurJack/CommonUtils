using System;
using System.Runtime.Serialization;

namespace CommonUtils.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a non-program error occurs.
    /// For example it's may be a business logic error or an validation error.
    /// </summary>
    [Serializable]
    public class UserException : ApplicationException
    {
        /// <summary>
        /// Constructor with the error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public UserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserException class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected UserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Constructor with the error message and inner exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}