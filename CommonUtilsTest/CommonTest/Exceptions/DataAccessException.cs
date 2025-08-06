//using System;
//using System.Runtime.Serialization;

//namespace CommonUtils.Exceptions
//{
//    /// <summary>
//    /// The exception that is thrown when a current user has not access to the data.
//    /// </summary>
//    [Serializable]
//    public class DataAccessException : AccessException
//    {
//        /// <summary>
//        /// Constructor without parameters.
//        /// Default message is CommonMessages.DataAccessDenied + Environment.NewLine + CommonMessages.AddressToAdministrator.
//        /// </summary>
//        public DataAccessException()
//            : this(CommonMessages.DataAccessDenied
//            + Environment.NewLine
//            + CommonMessages.AddressToAdministrator)
//        {
//        }

//        /// <summary>
//        /// Constructor with the error message.
//        /// </summary>
//        /// <param name="message">Error message.</param>
//        public DataAccessException(string message)
//            : base(message)
//        {
//        }


//        /// <summary>
//        /// Initializes a new instance of the DataAccessException class with serialized data.
//        /// </summary>
//        /// <param name="info">The object that holds the serialized object data.</param>
//        /// <param name="context">The contextual information about the source or destination.</param>
//        protected DataAccessException(SerializationInfo info, StreamingContext context)
//            : base(info, context)
//        {
//        }
//    }
//}