//using System;

//namespace CommonUtils.Exceptions
//{
//    /// <summary>
//    /// Default class for exceptions catching and logging.
//    /// </summary>
//    public class DefaultExceptionManager : IExceptionManager
//    {
//        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

//        /// <summary>
//        /// Default constructor.
//        /// </summary>
//        public DefaultExceptionManager()
//        {
//            MailTo = Variables.DeveloperEmail;
//            MailFrom = Variables.UserEmail;
//        }


//        /// <summary>
//        /// Gets or sets the mail-to address.
//        /// Default value is Variables.DeveloperEmail.
//        /// </summary>
//        public string MailTo { get; set; }

//        /// <summary>
//        /// Gets or sets the mail-from address.
//        /// Default value is Variables.UserEmail.
//        /// </summary>
//        public string MailFrom { get; set; }

//        /// <summary>
//        /// Determinates is the exception will be catched by this exception manager.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        /// <returns>Returns true if exception will be catched; else returns false.</returns>
//        public bool IsCatcher(Exception ex) => true;

//        /// <summary>
//        /// Logging specified exception and all inner exceptions.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public void Log(Exception ex)
//        {
//            if (ex == null)
//            {
//                return;
//            }

//            var realException = ExceptionManager.GetRealException(ex);

//            if (ex is UnhandledException)
//            {
//                _logger.Fatal(realException);
//            }
//            else if (realException is UserException)
//            {
//                _logger.Warn(realException);
//            }
//            else
//            {
//                _logger.Error(realException);
//            }
//        }

//        /// <summary>
//        /// Shows information about exception.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public void Show(Exception ex)
//        {
//            if (ex == null
//                || !Environment.UserInteractive)
//            {
//                return;
//            }

//            Exception realException = ExceptionManager.GetRealException(ex);

//            CF.MessageBox(realException.Message
//                          + ((realException.InnerException == null) ? "" : "\n" + realException.InnerException.Message),
//                CommonMessageBoxImage.Warning);
//        }

//        /// <summary>
//        /// Sends the exception information to the developers.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public void Feedback(Exception ex)
//        {
//            if (!AllowFeedback(ex)) return;

//            //TODO: send email to developers. May be NLog ?
//        }


//        /// <summary>
//        /// Determinates is the exception manager allow feedback of the exception.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        /// <returns>Returns true if exception manager allow feedback,
//        /// else returns false.</returns>
//        protected virtual bool AllowFeedback(Exception ex)
//        {
//            return (!(ex is UserException)
//                && !String.IsNullOrEmpty(this.MailTo));
//        }
//    }
//}
