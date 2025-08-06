using System;

namespace CommonUtils.Event
{
    /// <summary>
    /// Delegate for any function without parameters
    /// </summary>
    public delegate void ProcedureCallback();


    /// <summary>
    /// Delegate for any function with parameters.
    /// </summary>
    /// <param name="args"></param>
    public delegate object ProcedureCallbackWithParams(params object[] args);

    /// <summary>
    /// Exception delegate.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public delegate void ExceptionEventHandler(Exception exception);

    /// <summary>
    /// Executor delegate.
    /// </summary>
    public delegate void ExecutorHandler(ExecutorEventArgs eventArgs);
}