namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// Interface for getting and setting custom configuration data from any sources.
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        string GetString(string path, string key);

        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        string GetString(ConfigName path, string key);

        /// <summary>
        /// Sets configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        void SetString(string path, string key, string value);

        /// <summary>
        /// Sets configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        void SetString(ConfigName path, string key, string value);
    }
}