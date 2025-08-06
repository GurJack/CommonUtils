using System;
using System.ComponentModel;

namespace CommonUtils
{
    //How to use:

    //FormProgressBar frmProgress = new FormProgressBar ();
    //frmProgress.Show();
    //AsynchronousMethodHelper.AsynchronousExecution(new ThreadStart(delegate()
    //{
    //    // anonymous  method for asynchronous execution
    //    for (int i = 0; i <= 2; i++)
    //    {
    //        // fake execution  (delay 2 seconds)
    //        Thread.Sleep(1000);
    //    }
    //}), new ThreadStart(delegate()
    //{
    //    // call back
    //    frmProgress .Hide();
    //}));


    /// <summary>
    /// AsynchronousMethodHelper.
    /// </summary>
    public static class AsynchronousMethodHelper
    {
        private static Delegate _method;
        private static Delegate _callBack;

        /// <summary>
        /// Execute async.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="callBack"></param>
        public static void AsynchronousExecution(Delegate method, Delegate callBack)
        {
            _method = method;
            _callBack = callBack;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerAsync();
        }

        private static void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _callBack.DynamicInvoke(null);
        }

        private static void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _method.DynamicInvoke(null);
        }
    }

}