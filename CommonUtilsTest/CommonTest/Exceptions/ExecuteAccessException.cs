//using System;
//using System.Runtime.Serialization;

//namespace CommonUtils.Exceptions
//{
//    /// <summary>
//    /// The exception that is thrown when a current user has not access to execute a operation.
//    /// </summary>
//    [Serializable]
//    public class ExecuteAccessException : AccessException
//    {
//        /// <summary>
//        /// Constructor without parameters.
//        /// Default message is CommonMessages.ExecuteAccessDenied + Environment.NewLine + CommonMessages.AddressToAdministrator.
//        /// </summary>
//        public ExecuteAccessException()
//            : this(CommonMessages.ExecuteAccessDenied
//                   + Environment.NewLine
//                   + CommonMessages.AddressToAdministrator)
//        {
//        }

//        /// <summary>
//        /// Constructor with the error message.
//        /// </summary>
//        /// <param name="message">Error message.</param>
//        public ExecuteAccessException(string message)
//            : base(message)
//        {
//        }


//        /// <summary>
//        /// Initializes a new instance of the ExecuteAccessException class with serialized data.
//        /// </summary>
//        /// <param name="info">The object that holds the serialized object data.</param>
//        /// <param name="context">The contextual information about the source or destination.</param>
//        protected ExecuteAccessException(SerializationInfo info, StreamingContext context)
//            : base(info, context)
//        {
//        }
//    }
//}