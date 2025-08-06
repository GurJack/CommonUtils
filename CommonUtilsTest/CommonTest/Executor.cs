using System;
using System.Reflection;
using CommonUtils.Event;
using CommonUtils.Exceptions;

namespace CommonUtils
{
    /// <summary>
    /// Static class for function executing
    /// </summary>
    public static class Executor
    {
        /// <summary>
        /// Occurs before the method will be invoked.
        /// </summary>
        public static event ExecutorHandler BeforeInvoke;

        /// <summary>
        /// Occurs after the method will be invoked.
        /// </summary>
        public static event ExecutorHandler AfterInvoke;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Executor()
        {
            Executor.BeforeInvoke += ExecutorOnBeforeInvoke;
            Executor.AfterInvoke += ExecutorOnAfterInvoke;
        }

        /// <summary>
        /// Executes any function with any parameters.
        /// Catching exception and showing exception dialog.
        /// </summary>
        /// <param name="method">Delegate for executing.</param>
        /// <returns>False if exception occured.</returns>
        public static bool DynamicInvoke(ProcedureCallback method)
        {
            return DynamicInvoke(method, null);
        }

        /// <summary>
        /// Executes any function with any parameters
        /// </summary>
        /// <param name="method">Delegate for executing</param>
        /// <param name="args">Function parameters</param>
        /// <returns>False if exception occured</returns>
        public static bool DynamicInvoke(Delegate method, params object[] args)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            var eventArgs = new ExecutorEventArgs(method);
            BeforeInvoke?.Invoke(eventArgs);
            try
            {
                method.DynamicInvoke(args);
            }
            catch (Exception e)
            {
                ExceptionManager.Publish(e);
                return false;
            }
            finally
            {
                AfterInvoke?.Invoke(eventArgs);
            }
            return true;
        }
        
        /// <summary>
        /// Executes any function with any parameters
        /// </summary>
        /// <param name="method">Method for executing</param>
        /// <param name="obj">The current object</param>
        /// <param name="args">Function parameters</param>
        /// <returns>False if exception occured</returns>
        public static bool Invoke(MethodBase method, object obj, params object[] args)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            var eventArgs = new ExecutorEventArgs(method);
            BeforeInvoke?.Invoke(eventArgs);
            try
            {
                method.Invoke(obj, args);
            }
            catch (Exception e)
            {
                ExceptionManager.Publish(e);
                return false;
            }
            finally
            {
                AfterInvoke?.Invoke(eventArgs);
            }
            return true;
        }

        /// <summary>
        /// Executes any action.
        /// </summary>
        /// <param name="action">Action for executing.</param>
        /// <returns>False if exception occured.</returns>
        public static bool Invoke(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var eventArgs = new ExecutorEventArgs(action);
            BeforeInvoke?.Invoke(eventArgs);
            try
            {
                action();
            }
            catch (Exception e)
            {
                ExceptionManager.Publish(e);
                return false;
            }
            finally
            {
                AfterInvoke?.Invoke(eventArgs);
            }
            return true;
        }


        /// <summary>
        /// Start the process.
        /// </summary>
        /// <param name="path"></param>
        public static void StartProcess(string path)
        {
            using (var process = new System.Diagnostics.Process())
            {
                try
                {
                    process.StartInfo.FileName = path;
                    process.Start();
                    process.WaitForInputIdle();
                }
                catch (Exception e)
                {
                    ExceptionManager.Publish(e);
                }
            }
        }

        private static void ExecutorOnBeforeInvoke(ExecutorEventArgs eventArgs)
        {
#if NET461 || NET47 || NET471 || NET472
            eventArgs.OriginalCursor = System.Windows.Forms.Cursor.Current;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
#endif
        }

        private static void ExecutorOnAfterInvoke(ExecutorEventArgs eventArgs)
        {
#if NET461 || NET47 || NET471 || NET472
            System.Windows.Forms.Cursor.Current = (System.Windows.Forms.Cursor) eventArgs.OriginalCursor;
#endif
        }
    }
}