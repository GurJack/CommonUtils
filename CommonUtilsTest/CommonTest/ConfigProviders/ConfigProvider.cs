using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// Static class for getting and setting the custom configuration data.
    /// May be used with a different implementations of the <see cref="IConfigProvider"/>.
    /// The instance of the <see cref="IConfigProvider"/> is define by URI prefix by the configuration path.
    /// </summary>
    public static class ConfigProvider
    {
        /// <summary>
        /// The system config URI prefix.
        /// </summary>
        public const string System = "system://";

        /// <summary>
        /// The user config URI prefix.
        /// </summary>
        public const string User = "user://";

        /// <summary>
        /// The user interface config URI prefix.
        /// </summary>

        public const string GUI = "gui://";

        /// <summary>
        /// The login config URI prefix.
        /// </summary>
        public const string Login = "login://";

        /// <summary>
        /// The private config URI prefix.
        /// </summary>
        public const string Private = "private://";


        private const string NotFound = "$NOT_FOUND$";

        /// <summary>
        /// Simple IoC container for <see cref="IConfigProvider"/> instances.
        /// </summary>
        private static readonly Dictionary<string, IConfigProvider> ConfigProviderHash = new Dictionary<string, IConfigProvider>();

        /// <summary>
        /// Static constructor.
        /// Sets default system config with "system://" prefix and 
        /// CryptoRegistryConfigProvider(CryptoProvider.GetCryptoProvider(CryptoType.SHA256), CompressionProvider.GetCompressionProvider(CompressionType.GZip)) instance.
        /// Sets default user config with "private://" prefix and 
        /// CryptoRegistryConfigProvider(new SHA256CryproProvider("СIS2.0?Privаtе#"), CompressionProvider.GetCompressionProvider(CompressionType.GZip)) instance.
        /// </summary>
        static ConfigProvider()
        {
#if NET461 || NET47 || NET471 || NET472
            SetConfigProvider(System,
                new CryptoRegistryConfigProvider(CryptoProvider.GetCryptoProvider(CryptoType.SHA256), CompressionProvider.GetCompressionProvider(CompressionType.GZip)));
            SetConfigProvider(Private,
                new CryptoRegistryConfigProvider(new SHA256CryproProvider("СIS2.0?Privаtе#"), CompressionProvider.GetCompressionProvider(CompressionType.GZip)));
#else
            SetConfigProvider(System,
                new CryptoXMLConfigProvider(Path.Combine(Information.ProgramPath, Information.ProgramName + ".system.config"),
                    CryptoProvider.GetCryptoProvider(CryptoType.SHA256), CompressionProvider.GetCompressionProvider(CompressionType.GZip)));
            SetConfigProvider(Private,
                new CryptoXMLConfigProvider(Path.Combine(Information.ProgramPath, Information.ProgramName + ".private.config"), new SHA256CryproProvider("СIS2.0?Privаtе#"),
                    CompressionProvider.GetCompressionProvider(CompressionType.GZip)));
#endif
        }

        /// <summary>
        /// Gets config instance by URI prefix.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <returns>Implementation of <see cref="IConfigProvider"/>.</returns>
        public static IConfigProvider GetConfigProvider(ConfigType configType)
        {
            return GetConfigProvider(UriHelper.EnumToPrefix(configType));
        }

        /// <summary>
        /// Gets config instance by URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <returns>Implementation of <see cref="IConfigProvider"/>.</returns>
        public static IConfigProvider GetConfigProvider(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((ICollection) ConfigProviderHash).SyncRoot)
            {
                if (ConfigProviderHash.ContainsKey(prefix))
                    return ConfigProviderHash[prefix];

                return null;
            }
        }

        /// <summary>
        /// Sets config instance with specified URI prefix.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="provider">Implementation of <see cref="IConfigProvider"/>.</param>
        public static void SetConfigProvider(ConfigType configType, IConfigProvider provider)
        {
            SetConfigProvider(UriHelper.EnumToPrefix(configType), provider);
        }
        
        /// <summary>
        /// Sets config instance with specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        /// <param name="provider">Implementation of <see cref="IConfigProvider"/>.</param>
        public static void SetConfigProvider(string prefix, IConfigProvider provider)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((ICollection) ConfigProviderHash).SyncRoot)
            {
                if (provider == null)
                {
                    RemoveConfigProvider(prefix);
                }
                else
                {
                    ConfigProviderHash[prefix] = provider;
                }
            }
        }

        /// <summary>
        /// Removes the config instance with the specified URI prefix.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        public static void RemoveConfigProvider(ConfigType configType)
        {
            RemoveConfigProvider(UriHelper.EnumToPrefix(configType));
        }

        /// <summary>
        /// Removes the config instance with the specified URI prefix.
        /// </summary>
        /// <param name="prefix">The specified URI prefix.</param>
        public static void RemoveConfigProvider(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            lock (((ICollection) ConfigProviderHash).SyncRoot)
            {
                ConfigProviderHash.Remove(prefix);
            }
        }


        /// <summary>
        /// Gets the all configs prefix list.
        /// </summary>
        /// <returns>The prefix list for all configs.</returns>
        public static string[] GetConfigProviderPrefixList()
        {
            lock (((ICollection) ConfigProviderHash).SyncRoot)
            {
                return ConfigProviderHash.Select(c => c.Key).ToArray();
            }
        }

        /// <summary>
        /// Saves configs from cache to store.
        /// <param name="configType">The specified config type.</param>
        /// </summary>
        public static void SaveConfigProviderData(ConfigType configType)
        {
            var provider = GetConfigProvider(configType);
            (provider as ICacheableConfigProvider)?.Save();
        }

        /// <summary>
        /// Saves configs from cache to store.
        /// <param name="prefix">The specified URI prefix.</param>
        /// </summary>
        public static void SaveConfigProviderData(string prefix)
        {
            var provider = GetConfigProvider(prefix);
            (provider as ICacheableConfigProvider)?.Save();
        }

        /// <summary>
        /// Saves configs from cache to store.
        /// </summary>
        public static void SaveAllConfigProvidersData()
        {
            lock (((ICollection) ConfigProviderHash).SyncRoot)
            {
                foreach (var entry in ConfigProviderHash)
                {
                    var configProvider = entry.Value;

                    if (configProvider is ICacheableConfigProvider)
                        ((ICacheableConfigProvider) configProvider).Save();
                }
            }
        }

        /// <summary>
        /// Resets configs in store.
        /// </summary>
        public static void ResetSettings()
        {
            lock (((ICollection) ConfigProviderHash).SyncRoot)
            {
                foreach (var entry in ConfigProviderHash)
                {
                    var configProvider = entry.Value;

                    if (configProvider is ICacheableConfigProvider)
                        ((ICacheableConfigProvider) configProvider).Reset();
                }
            }
        }


        /// <summary>
        /// Checking the configuration value existing.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>True if value exists; otherwise, false.</returns>
        public static bool ExistsKey(string path, string key)
        {
            return GetString(path, key, NotFound) != NotFound;
        }

        /// <summary>
        /// Checking the configuration value existing.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>True if value exists; otherwise, false.</returns>
        public static bool ExistsKey(ConfigType configType, ConfigName key)
        {
            return ExistsKey(UriHelper.EnumToPrefix(configType), key.ToString());
        }


        /// <summary>
        /// Removes configuration value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        public static void RemoveKey(string path, string key)
        {
            SetString(path, key, null);
        }

        /// <summary>
        /// Removes configuration value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        public static void RemoveKey(ConfigType configType, ConfigName key)
        {
            RemoveKey(UriHelper.EnumToPrefix(configType), key.ToString());
        }

        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default string value.</param>
        /// <returns>String value from configuration data.
        /// If string value is not found then default value will be returned.</returns>
        public static string GetString(string path, string key, string defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var configProvider = CheckConfigProviderByPath(path);

            lock (configProvider)
            {
                var value = configProvider.GetString(path, key);
                if (value == null)
                {
                    return defaultValue;
                }

                return value;
            }
        }

        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default string value.</param>
        /// <returns>String value from configuration data.
        /// If string value is not found then default value will be returned.</returns>
        public static string GetString(ConfigType configType, ConfigName key, string defaultValue)
        {
            return GetString(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }


        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>

        public static string GetString(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetString(path, key, String.Empty);
        }

        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>

        public static string GetString(ConfigType configType, ConfigName key)
        {
            return GetString(UriHelper.EnumToPrefix(configType), key.ToString());
        }


        /// <summary>
        /// Sets configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        public static void SetString(string path, string key, string value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var configProvider = CheckConfigProviderByPath(path);

            lock (configProvider)
            {
                var oldValue = configProvider.GetString(path, key);
                if (value == oldValue)
                {
                    return;
                }

                configProvider.SetString(path, key, value);
            }
        }

        /// <summary>
        /// Sets configuration string value by specified path and key.
        /// Removes the key if value is null.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        public static void SetString(ConfigType configType, ConfigName key, string value)
        {
            SetString(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }

        /// <summary>
        /// Gets configuration string list by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value list from configuration data.</returns>
        public static string[] GetStringList(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var list = new ArrayList();
            var count = 0;
            string value;
            while ((value = GetString(path, key + count, NotFound)) != NotFound)
            {
                list.Add(value);
                count++;
            }

            return (string[]) list.ToArray(typeof(string));
        }

        /// <summary>
        /// Gets configuration string list by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value list from configuration data.</returns>
        public static string[] GetStringList(ConfigType configType, ConfigName key)
        {
            return GetStringList(UriHelper.EnumToPrefix(configType), key.ToString());
        }

        /// <summary>
        /// Sets configuration string list by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="valueList">Specified string list.</param>
        public static void SetStringList(string path, string key, string[] valueList)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Remove old values.
            var count = 0;
            while (ExistsKey(path, key + count))
            {
                RemoveKey(path, key + count);
                count++;
            }

            // Add new values.
            for (var i = 0; i < valueList.Length; i++)
            {
                SetString(path, key + i, valueList[i]);
            }
        }

        /// <summary>
        /// Sets configuration string list by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="valueList">Specified string list.</param>
        public static void SetStringList(ConfigType configType, ConfigName key, string[] valueList)
        {
            SetStringList(UriHelper.EnumToPrefix(configType), key.ToString(), valueList);
        }

        /// <summary>
        /// Gets configuration decimal value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default decimal value.</param>
        /// <returns>Decimal value from configuration data.
        /// If decimal value is not found then default value will be returned.</returns>
        public static decimal GetDecimal(string path, string key, decimal defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = GetString(path, key, null);
            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return XmlConvert.ToDecimal(value);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
            catch (OverflowException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets configuration decimal value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default decimal value.</param>
        /// <returns>Decimal value from configuration data.
        /// If decimal value is not found then default value will be returned.</returns>
        public static decimal GetDecimal(ConfigType configType, ConfigName key, decimal defaultValue)
        {
            return GetDecimal(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }

        /// <summary>
        /// Gets configuration decimal value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Decimal value from configuration data.</returns>
        public static decimal GetDecimal(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetDecimal(path, key, 0);
        }


        /// <summary>
        /// Gets configuration decimal value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Decimal value from configuration data.</returns>
        public static decimal GetDecimal(ConfigType configType, ConfigName key)
        {
            return GetDecimal(UriHelper.EnumToPrefix(configType), key.ToString());
        }

        /// <summary>
        /// Sets configuration decimal value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified decimal value.</param>
        public static void SetDecimal(string path, string key, decimal value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            SetString(path, key, XmlConvert.ToString(value));
        }

        /// <summary>
        /// Sets configuration decimal value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified decimal value.</param>
        public static void SetDecimal(ConfigType configType, ConfigName key, decimal value)
        {
            SetDecimal(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }

        /// <summary>
        /// Gets configuration double value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default double value.</param>
        /// <returns>Double value from configuration data.
        /// If double value is not found then default value will be returned.</returns>
        public static double GetDouble(string path, string key, double defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = GetString(path, key, null);
            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return XmlConvert.ToDouble(value);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
            catch (OverflowException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets configuration double value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default double value.</param>
        /// <returns>Double value from configuration data.
        /// If double value is not found then default value will be returned.</returns>
        public static double GetDouble(ConfigType configType, ConfigName key, double defaultValue)
        {
            return GetDouble(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }

        /// <summary>
        /// Gets configuration double value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Double value from configuration data.</returns>
        public static double GetDouble(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetDouble(path, key, 0);
        }


        /// <summary>
        /// Gets configuration double value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Double value from configuration data.</returns>
        public static double GetDouble(ConfigType configType, ConfigName key)
        {
            return GetDouble(UriHelper.EnumToPrefix(configType), key.ToString());
        }

        /// <summary>
        /// Sets configuration double value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified double value.</param>
        public static void SetDouble(string path, string key, double value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            SetString(path, key, XmlConvert.ToString(value));
        }

        /// <summary>
        /// Sets configuration double value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified double value.</param>
        public static void SetDouble(ConfigType configType, ConfigName key, double value)
        {
            SetDouble(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }

        /// <summary>
        /// Gets configuration boolean value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default boolean value.</param>
        /// <returns>Boolean value from configuration data.
        /// If boolean value is not found then default value will be returned.</returns>
        public static bool GetBoolean(string path, string key, bool defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = GetString(path, key, null);
            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return XmlConvert.ToBoolean(value);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets configuration boolean value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default boolean value.</param>
        /// <returns>Boolean value from configuration data.
        /// If boolean value is not found then default value will be returned.</returns>
        public static bool GetBoolean(ConfigType configType, ConfigName key, bool defaultValue)
        {
            return GetBoolean(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }


        /// <summary>
        /// Gets configuration boolean value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Boolean value from configuration data.</returns>
        public static bool GetBoolean(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetBoolean(path, key, false);
        }

        /// <summary>
        /// Gets configuration boolean value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Boolean value from configuration data.</returns>
        public static bool GetBoolean(ConfigType configType, ConfigName key)
        {
            return GetBoolean(UriHelper.EnumToPrefix(configType), key.ToString());
        }


        /// <summary>
        /// Sets configuration boolean value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified boolean value.</param>
        public static void SetBoolean(string path, string key, bool value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            SetString(path, key, XmlConvert.ToString(value));
        }


        /// <summary>
        /// Sets configuration boolean value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified boolean value.</param>
        public static void SetBoolean(ConfigType configType, ConfigName key, bool value)
        {
            SetBoolean(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }


        /// <summary>
        /// Gets configuration DateTime value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default DateTime value.</param>
        /// <returns>DateTime value from configuration data.
        /// If DateTime value is not found then default value will be returned.</returns>
        public static DateTime GetDateTime(string path, string key, DateTime defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = GetString(path, key, null);
            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets configuration DateTime value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default DateTime value.</param>
        /// <returns>DateTime value from configuration data.
        /// If DateTime value is not found then default value will be returned.</returns>
        public static DateTime GetDateTime(ConfigType configType, ConfigName key, DateTime defaultValue)
        {
            return GetDateTime(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }


        /// <summary>
        /// Gets configuration DateTime value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>DateTime value from configuration data.</returns>
        public static DateTime GetDateTime(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetDateTime(path, key, DateTime.MinValue);
        }


        /// <summary>
        /// Gets configuration DateTime value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>DateTime value from configuration data.</returns>
        public static DateTime GetDateTime(ConfigType configType, ConfigName key)
        {
            return GetDateTime(UriHelper.EnumToPrefix(configType), key.ToString());
        }

        /// <summary>
        /// Sets configuration DateTime value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified DateTime value.</param>
        public static void SetDateTime(string path, string key, DateTime value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            SetString(path, key, XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
        }

        /// <summary>
        /// Sets configuration DateTime value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified DateTime value.</param>
        public static void SetDateTime(ConfigType configType, ConfigName key, DateTime value)
        {
            SetDateTime(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default binary value.</param>
        /// <returns>Binary value from configuration data.
        /// If binary value is not found then default value will be returned.</returns>
        public static byte[] GetBinary(string path, string key, byte[] defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = GetString(path, key, null);
            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.FromBase64String(value);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default binary value.</param>
        /// <returns>Binary value from configuration data.
        /// If binary value is not found then default value will be returned.</returns>
        public static byte[] GetBinary(ConfigType configType, ConfigName key, byte[] defaultValue)
        {
            return GetBinary(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }


        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Binary value from configuration data.</returns>
        public static byte[] GetBinary(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetBinary(path, key, new byte[0]);
        }

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Binary value from configuration data.</returns>
        public static byte[] GetBinary(ConfigType configType, ConfigName key)
        {
            return GetBinary(UriHelper.EnumToPrefix(configType), key.ToString());
        }


        /// <summary>
        /// Sets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified binary value.</param>
        public static void SetBinary(string path, string key, byte[] value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            SetString(path, key, Convert.ToBase64String(value));
        }

        /// <summary>
        /// Sets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified binary value.</param>
        public static void SetBinary(ConfigType configType, ConfigName key, byte[] value)
        {
            SetBinary(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }

        /// <summary>
        /// Gets configuration Guid value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default Guid value.</param>
        /// <returns>Guid value from configuration data.
        /// If Guid value is not found then default value will be returned.</returns>
        public static Guid GetGuid(string path, string key, Guid defaultValue)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var value = GetString(path, key, null);
            if (value == null ||
                value == "0")
            {
                return defaultValue;
            }

            try
            {
                return XmlConvert.ToGuid(value);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets configuration Guid value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default Guid value.</param>
        /// <returns>Guid value from configuration data.
        /// If Guid value is not found then default value will be returned.</returns>
        public static Guid GetGuid(ConfigType configType, ConfigName key, Guid defaultValue)
        {
            return GetGuid(UriHelper.EnumToPrefix(configType), key.ToString(), defaultValue);
        }


        /// <summary>
        /// Gets configuration Guid value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Guid value from configuration data.</returns>
        public static Guid GetGuid(string path, string key)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetGuid(path, key, Guid.Empty);
        }


        /// <summary>
        /// Gets configuration Guid value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Guid value from configuration data.</returns>
        public static Guid GetGuid(ConfigType configType, ConfigName key)
        {
            return GetGuid(UriHelper.EnumToPrefix(configType), key.ToString());
        }

        /// <summary>
        /// Sets configuration Guid value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified Guid value.</param>
        public static void SetGuid(string path, string key, Guid value)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            SetString(path, key, XmlConvert.ToString(value));
        }

        /// <summary>
        /// Sets configuration Guid value by specified path and key.
        /// </summary>
        /// <param name="configType">The specified config type.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified Guid value.</param>
        public static void SetGuid(ConfigType configType, ConfigName key, Guid value)
        {
            SetGuid(UriHelper.EnumToPrefix(configType), key.ToString(), value);
        }

        /// <summary>
        /// Gets config provider by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        /// <returns>Implementation of <see cref="IConfigProvider"/>.</returns>
        private static IConfigProvider CheckConfigProviderByPath(string path)
        {
            var configProvider = GetConfigByPath(path);
            if (configProvider == null)
            {
                throw new ArgumentException(String.Format(CommonMessages.ConfigProviderNotFound, path));
            }

            return configProvider;
        }

        /// <summary>
        /// Gets config instance by specified path.
        /// </summary>
        /// <param name="path">The specified path.</param>
        private static IConfigProvider GetConfigByPath(string path)
        {
            return GetConfigProvider(UriHelper.GetPrefix(path));
        }
    }
}