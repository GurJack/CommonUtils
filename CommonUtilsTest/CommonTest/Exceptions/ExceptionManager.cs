//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using CommonUtils.Event;

//namespace CommonUtils.Exceptions
//{
//    /// <summary>
//    /// Static class for exceptions catching.
//    /// Logging exceptions and shows message about it.
//    /// </summary>
//    public static class ExceptionManager
//    {
//        /// <summary>
//        /// List of <see cref="IExceptionManager"/> instances.
//        /// </summary>
//        private static readonly List<IExceptionManager> ExceptionManagerList = new List<IExceptionManager>();

//        /// <summary>
//        /// Occurs before the exception is processed.
//        /// </summary>
//        public static event ExceptionEventHandler BeforeExceptionProcessing;

//        /// <summary>
//        /// Occurs after the exception is processed.
//        /// </summary>
//        public static event ExceptionEventHandler AfterExceptionProcessing;

//        /// <summary>
//        /// Static constructor.
//        /// Sets default exception manager.
//        /// </summary>
//        static ExceptionManager()
//        {
//            AddExceptionManager(new DefaultExceptionManager());
//        }

//        /// <summary>
//        /// Inserts exception manager into the top of the list.
//        /// </summary>
//        /// <param name="exceptionManager">Implementation of the <see cref="IExceptionManager"/>.</param>
//        public static void AddExceptionManager(IExceptionManager exceptionManager)
//        {
//            if (exceptionManager == null)
//            {
//                throw new ArgumentNullException(nameof(exceptionManager));
//            }

//            lock (((ICollection) ExceptionManagerList).SyncRoot)
//            {
//                ExceptionManagerList.Insert(0, exceptionManager);
//            }
//        }

//        /// <summary>
//        /// Removes exception manager from the list.
//        /// </summary>
//        /// <param name="exceptionManager">Implementation of the <see cref="IExceptionManager"/>.</param>
//        public static void RemoveExceptionManager(IExceptionManager exceptionManager)
//        {
//            if (exceptionManager == null)
//            {
//                throw new ArgumentNullException(nameof(exceptionManager));
//            }

//            lock (((ICollection) ExceptionManagerList).SyncRoot)
//            {
//                if (ExceptionManagerList.Contains(exceptionManager))
//                {
//                    ExceptionManagerList.Remove(exceptionManager);
//                }
//            }
//        }

//        /// <summary>
//        /// Gets the all exception manager list.
//        /// </summary>
//        /// <returns>The all exception manager list.</returns>
//        public static List<IExceptionManager> GetExceptionManagerList()
//        {
//            lock (((ICollection) ExceptionManagerList).SyncRoot)
//            {
//                return ExceptionManagerList.ToList();
//            }
//        }

//        /// <summary>
//        /// Logging, shows and feedback information about exception.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public static void Publish(Exception ex)
//        {
//            BeforeExceptionProcessing?.Invoke(ex);

//            Log(ex, false);
//            Show(ex, false);
//            Feedback(ex, false);

//            AfterExceptionProcessing?.Invoke(ex);
//        }

//        /// <summary>
//        /// Logging specified exception and all inner exceptions.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public static void Log(Exception ex)
//        {
//            Log(ex, true);
//        }

//        private static void Log(Exception ex, bool needEventOccurs)
//        {
//            if (needEventOccurs)
//            {
//                BeforeExceptionProcessing?.Invoke(ex);
//            }

//            if (ex == null)
//            {
//                return;
//            }

//            //todo: перед проверкой ex is System.AggregateException надо получить реальное исключение (не оболочку)!
//            //var realException = ExceptionManager.GetRealException(ex);

//            if (ex is AggregateException exception)
//            {
//                foreach (var innerException in exception.Flatten().InnerExceptions)
//                {
//                    Log(innerException, needEventOccurs);
//                }

//                return;
//            }

//            foreach (var exceptionManager in GetExceptionManagerList())
//            {
//                if (exceptionManager.IsCatcher(ex))
//                    lock (exceptionManager)
//                    {
//                        exceptionManager.Log(ex);
//                    }
//            }

//            if (needEventOccurs)
//            {
//                AfterExceptionProcessing?.Invoke(ex);
//            }
//        }

//        /// <summary>
//        /// Shows information about exception.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public static void Show(Exception ex)
//        {
//            Show(ex, true);
//        }

//        private static void Show(Exception ex, bool needEventOccurs)
//        {
//            if (needEventOccurs)
//            {
//                BeforeExceptionProcessing?.Invoke(ex);
//            }

//            if (ex == null)
//            {
//                return;
//            }

//            if (ex is AggregateException exception)
//            {
//                foreach (var innerException in exception.Flatten().InnerExceptions)
//                {
//                    Show(innerException, needEventOccurs);
//                }

//                return;
//            }

//            foreach (var exceptionManager in GetExceptionManagerList())
//            {
//                if (exceptionManager.IsCatcher(ex))
//                    lock (exceptionManager)
//                    {
//                        exceptionManager.Show(ex);
//                    }
//            }

//            if (needEventOccurs)
//            {
//                AfterExceptionProcessing?.Invoke(ex);
//            }
//        }

//        /// <summary>
//        /// Sends the exception information to the developers.
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        public static void Feedback(Exception ex)
//        {
//            Feedback(ex, true);
//        }

//        private static void Feedback(Exception ex, bool needEventOccurs)
//        {
//            if (needEventOccurs)
//            {
//                BeforeExceptionProcessing?.Invoke(ex);
//            }

//            if (ex == null)
//            {
//                return;
//            }

//            if (ex is AggregateException exception)
//            {
//                foreach (var innerException in exception.Flatten().InnerExceptions)
//                {
//                    Feedback(innerException, needEventOccurs);
//                }

//                return;
//            }

//            foreach (var exceptionManager in GetExceptionManagerList())
//            {
//                if (exceptionManager.IsCatcher(ex))
//                    lock (exceptionManager)
//                    {
//                        exceptionManager.Feedback(ex);
//                    }
//            }

//            if (needEventOccurs)
//            {
//                AfterExceptionProcessing?.Invoke(ex);
//            }
//        }

//        /// <summary>
//        /// Gets the real (inner) exception from the specified exception.
//        /// Показать реальное исключение, а не оболочку исключения (System.Reflection.TargetInvocationException либо System.TypeInitializationException).
//        /// </summary>
//        /// <param name="ex">Specified exception.</param>
//        /// <returns>The real exception.</returns>
//        public static Exception GetRealException(Exception ex)
//        {
//            if (ex == null)
//            {
//                return null;
//            }

//            while ((ex is System.Reflection.TargetInvocationException
//                    || ex is System.TypeInitializationException
//                    || ex is UnhandledException)
//                   && ex.InnerException != null)
//            {
//                ex = ex.InnerException;
//            }

//            if (ex is System.AggregateException)
//            {
//                ex = ((System.AggregateException) ex).Flatten();
//                return GetRealException(ex.InnerException);
//            }

//            return ex;
//        }
//    }
//}