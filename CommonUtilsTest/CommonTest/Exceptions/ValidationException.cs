using System;
using System.Runtime.Serialization;

namespace CommonUtils.Exceptions
{
    /// <summary>
    /// The exception that is thrown when
    /// a user data is not valid.
    /// </summary>
    [Serializable]
    public class ValidationException : UserException
    {
        /// <summary>
        /// Constructor with the error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public ValidationException(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the ValidationException class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}