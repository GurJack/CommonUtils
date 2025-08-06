namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// The fake config provider.
    /// </summary>
    public class FakeConfigProvider : IConfigProvider
    {
        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        public string GetString(string path, string key)
        {
            return null;
        }

        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        public string GetString(ConfigName path, string key)
        {
            return null;
        }

        /// <summary>
        /// Sets configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        public void SetString(string path, string key, string value)
        {
        }

        /// <summary>
        /// Sets configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        public void SetString(ConfigName path, string key, string value)
        {
        }
    }
}