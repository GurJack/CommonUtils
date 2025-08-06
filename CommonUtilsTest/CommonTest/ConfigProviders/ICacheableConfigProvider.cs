namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// Interface for getting and setting custom configuration data from any sources with cache support.
    /// </summary>
    public interface ICacheableConfigProvider : IConfigProvider
    {
        /// <summary>
        /// Saves configs from cache to store.
        /// </summary>
        void Save();

        /// <summary>
        /// Resets configs in store.
        /// </summary>
        void Reset();
    }
}