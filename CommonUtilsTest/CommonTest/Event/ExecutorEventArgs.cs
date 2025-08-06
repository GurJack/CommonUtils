namespace CommonUtils.Event
{
    /// <summary>
    /// The event data for the execute function.
    /// </summary>
    public class ExecutorEventArgs : System.EventArgs
    {
        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="method"></param>
        public ExecutorEventArgs(object method)
        {
            Method = method;
        }

        /// <summary>
        /// Gets the method for execute.
        /// </summary>
        public object Method { get; }

        /// <summary>
        /// Gets or sets the original cursor.
        /// </summary>
        public object OriginalCursor { get; set; }
    }
}