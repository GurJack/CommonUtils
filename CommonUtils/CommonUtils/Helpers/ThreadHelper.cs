//using System;
//using System.Threading;

//namespace CommonUtils.Helpers
//{
//    /// <summary>
//    /// Thread helper.
//    /// </summary>
//    public static class ThreadHelper
//    {
//        private static bool _isUserInteractive;

//        /// <summary>
//        ///  Gets a value indicating whether the current process is running in user interactive mode.
//        /// </summary>
//        public static bool UserInteractive => Environment.UserInteractive && (_isUserInteractive);

//        /// <summary>
//        ///  Sets the value indicating whether the current process is running in user interactive mode.
//        /// </summary>
//        public static bool SetUserInteractive(bool value) => _isUserInteractive = value;

//        /// <summary>
//        /// Execute action dependent with the user interface.
//        /// </summary>
//        /// <param name="uiAction"></param>
//        public static void ExecuteUI(ThreadStart uiAction)
//        {
//            if (!UserInteractive) return;

//            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
//            {
//                uiAction();
//            }
//            else
//            {
//                var thread = new System.Threading.Thread(uiAction);
//                thread.SetApartmentState(ApartmentState.STA);
//                thread.Start();
//                thread.Join();
//            }
//        }

//        /// <summary>
//        /// Execute function dependent with the user interface.
//        /// </summary>
//        public static T ExecuteUI<T>(Func<T> func)
//        {
//            if (!UserInteractive) return default(T);

//            if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
//            {
//                return func();
//            }
//            else
//            {
//                var obj = new ThreadResult<T>();
//                var thread = new System.Threading.Thread(() => { ExecuteFunc(func, obj); });
//                thread.SetApartmentState(ApartmentState.STA);
//                thread.Start();
//                thread.Join();

//                return obj.Result;
//            }
//        }

//        /// <summary>
//        /// Execute action dependent with the user interface.
//        /// </summary>
//        private static void ExecuteFunc<T>(Func<T> func, ThreadResult<T> obj)
//        {
//            obj.Result = func();
//        }

//        private class ThreadResult<T>
//        {
//            public T Result { get; set; }
//        }
//    }
//}