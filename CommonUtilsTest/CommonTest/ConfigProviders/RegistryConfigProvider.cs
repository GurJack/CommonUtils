#if NET461 || NET47 || NET471 || NET472
using System;
using Microsoft.Win32;

namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// Class for getting and setting custom configuration data.
    /// Uses the Windows Registry.
    /// Default root registry key is HKEY_CURRENT_USER.
    /// Default root path is Software/CompanyName/ProductName.
    /// </summary>
    public class RegistryConfigProvider : IConfigProvider
    {
        /// <summary>
        /// Constant for default root path.
        /// </summary>
        public const string DefaultRootPath = "Software";

        /// <summary>
        /// Default root registry.
        /// </summary>
        public static readonly RegistryKey DefaultRoot = Registry.CurrentUser;


        /// <summary>
        /// Default constructor.
        /// </summary>
        public RegistryConfigProvider() : this(DefaultRoot)
        {
        }

        /// <summary>
        /// Constructor with a root registry key.
        /// </summary>
        /// <param name="rootKey">The root registry key.</param>
        public RegistryConfigProvider(RegistryKey rootKey)
            : this(rootKey, DefaultRootPath + UriHelper.PathSeparator + Information.CompanyName + UriHelper.PathSeparator + Information.ProductName)
        {
        }

        /// <summary>
        /// Constructor with a root registry key and path.
        /// </summary>
        /// <param name="rootKey">The root registry key.</param>
        /// <param name="rootPath">The path from the root key to the config data.</param>
        public RegistryConfigProvider(RegistryKey rootKey, string rootPath)
        {
            if (rootKey == null)
            {
                throw new ArgumentNullException(nameof(rootKey));
            }
            if (rootPath == null)
            {
                rootPath = String.Empty;
            }

            RootKey = rootKey;
            RootPath = rootPath;
        }


        /// <summary>
        /// Gets the root registry key.
        /// </summary>
        public RegistryKey RootKey { get; }

        /// <summary>
        /// Gets the path from the root key to the config data.
        /// </summary>
        public string RootPath { get; }


        /// <summary>
        /// Gets the configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        public virtual string GetString(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var startPath = GetStartPath(path);

            var registryKey = GetRegistryKey(RootKey, startPath);
            if (registryKey == null)
            {
                return null;
            }

            try
            {
                return GetValueFromRegistry(registryKey.GetValue(key)?.ToString());
            }
            finally
            {
                registryKey.Close();
            }
        }

        /// <summary>
        /// Gets the configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        public String GetString(ConfigName path, String key)
        {
            return GetString(path.ToString(), key);
        }

        /// <summary>
        /// Sets the configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        public virtual void SetString(string path, string key, string value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null
                && GetString(path, key) == null)
            {
                return;
            }

            var startPath = GetStartPath(path);

            var registryKey = CreateRegistryKey(RootKey, startPath);
            if (registryKey == null)
            {
                throw new ArgumentException(String.Format(CommonMessages.InvalidConfigPath, startPath));
            }

            try
            {
                if (value == null)
                {
                    registryKey.DeleteValue(key);
                }
                else
                {
                    registryKey.SetValue(key, GetValueToRegistry(value));
                }
            }
            finally
            {
                registryKey.Close();
            }
        }

        /// <summary>
        /// Sets the configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        public void SetString(ConfigName path, String key, String value)
        {
            SetString(path.ToString(), key, value);
        }

        /// <summary>
        /// Gets value from registry.
        /// </summary>
        /// <param name="value">Raw value readed from registry.</param>
        /// <returns>String from registry.</returns>
        protected virtual String GetValueFromRegistry(String value)
        {
            return value;
        }

        /// <summary>
        /// Gets value for writing to registry.
        /// </summary>
        /// <param name="value">Raw value.</param>
        /// /// <returns>String value for writing to registry.</returns>
        protected virtual String GetValueToRegistry(String value)
        {
            return value;
        }

        private string GetStartPath(string path)
        {
            path = UriHelper.GetTail(path);

            if (RootPath.Length > 0)
            {
                return UriHelper.Combine(RootPath, path);
            }

            return path;
        }

        private RegistryKey GetRegistryKey(RegistryKey parentKey, string path)
        {
            if (path.Length == 0)
            {
                return null;
            }

            int separatorIndex = path.IndexOf(UriHelper.PathSeparator);
            if (separatorIndex >= 0)
            {
                var currentKey = parentKey.OpenSubKey(path.Substring(0, separatorIndex));
                if (currentKey == null)
                {
                    return null;
                }

                try
                {
                    return GetRegistryKey(currentKey, path.Substring(separatorIndex + 1, path.Length - separatorIndex - 1));
                }
                finally
                {
                    currentKey.Close();
                }
            }
            return parentKey.OpenSubKey(path);
        }

        private RegistryKey CreateRegistryKey(RegistryKey parentKey, string path)
        {
            if (path.Length == 0)
            {
                return null;
            }

            int separatorIndex = path.IndexOf(UriHelper.PathSeparator);
            if (separatorIndex >= 0)
            {
                var currentKey = parentKey.CreateSubKey(path.Substring(0, separatorIndex));
                if (currentKey == null)
                {
                    return null;
                }

                try
                {
                    return CreateRegistryKey(currentKey, path.Substring(separatorIndex + 1, path.Length - separatorIndex - 1));
                }
                finally
                {
                    currentKey.Close();
                }
            }
            return parentKey.CreateSubKey(path);
        }
    }
}
#endif