namespace CommonUtils
{
    /// <summary>
    /// Interface for the objects with an unique key
    /// and a text label.
    /// </summary>
    public interface IKeyLabel : IKey
    {
        /// <summary>
        /// Gets the text label of the object.
        /// </summary>
        string Label { get; }
    }
}