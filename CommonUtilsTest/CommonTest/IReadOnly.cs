namespace CommonUtils
{
    /// <summary>
    /// Interface for readOnly supporting.
    /// </summary>
    public interface IReadOnly
    {
        /// <summary>
        /// Gets or sets a value indicating whether the control is read only
        /// </summary>
        bool ReadOnly { get; set; }
    }
}