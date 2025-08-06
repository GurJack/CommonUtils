//using System;
//using System.Runtime.Serialization;

//namespace CommonUtils.Exceptions
//{
//    /// <summary>
//    /// The exception that is thrown when a current user has not any access.
//    /// </summary>
//    [Serializable]
//    public class AccessException : UserException
//    {
//        /// <summary>
//        /// Constructor without parameters.
//        /// Default message is CommonMessages.AccessDenied + Environment.NewLine + CommonMessages.AddressToAdministrator.
//        /// </summary>
//        public AccessException()
//            : this(CommonMessages.AccessDenied
//                   + Environment.NewLine
//                   + CommonMessages.AddressToAdministrator)
//        {
//        }

//        /// <summary>
//        /// Constructor with the error message.
//        /// </summary>
//        /// <param name="message">Error message.</param>
//        public AccessException(string message)
//            : base(message)
//        {
//        }


//        /// <summary>
//        /// Initializes a new instance of the AccessException class with serialized data.
//        /// </summary>
//        /// <param name="info">The object that holds the serialized object data.</param>
//        /// <param name="context">The contextual information about the source or destination.</param>
//        protected AccessException(SerializationInfo info, StreamingContext context)
//            : base(info, context)
//        {
//        }
//    }
//}