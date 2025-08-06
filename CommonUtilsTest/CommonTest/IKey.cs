namespace CommonUtils
{
    /// <summary>
    /// Interface for the objects with the unique key property.
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// Gets the key of the object.
        /// </summary>
        object Key { get; }

        /// <summary>
        /// Gets the unique full key of the object.
        /// </summary>
        string FullKey { get; }
    }
}