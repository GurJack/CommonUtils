using System;
using CommonUtils.ConfigProviders;

namespace CommonUtils
{
    /// <summary>
    /// The data for a control configuration.
    /// </summary>
    public interface IControlConfig
    {
        /// <summary>
        /// Gets configuration string value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default string value.</param>
        /// <returns>String value from configuration data.
        /// If string value is not found then default value will be returned.</returns>
        string GetString(string key, string defaultValue);

        /// <summary>
        /// Gets configuration string value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default string value.</param>
        /// <returns>String value from configuration data.
        /// If string value is not found then default value will be returned.</returns>
        string GetString(ConfigName key, string defaultValue);

        /// <summary>
        /// Gets configuration string value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        string GetString(string key);

        /// <summary>
        /// Gets configuration string value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        string GetString(ConfigName key);

        /// <summary>
        /// Sets configuration string value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        void SetString(string key, string value);

        /// <summary>
        /// Sets configuration string value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified string value.</param>
        void SetString(ConfigName key, string value);

        /// <summary>
        /// Gets configuration string list by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value list from configuration data.</returns>
        string[] GetStringList(string key);

        /// <summary>
        /// Gets configuration string list by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value list from configuration data.</returns>
        string[] GetStringList(ConfigName key);

        /// <summary>
        /// Sets configuration string list by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="valueList">Specified string list.</param>
        void SetStringList(string key, string[] valueList);

        /// <summary>
        /// Sets configuration string list by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="valueList">Specified string list.</param>
        void SetStringList(ConfigName key, string[] valueList);

        /// <summary>
        /// Gets configuration decimal value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default decimal value.</param>
        /// <returns>Decimal value from configuration data.
        /// If decimal value is not found then default value will be returned.</returns>
        decimal GetDecimal(string key, decimal defaultValue);

        /// <summary>
        /// Gets configuration decimal value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default decimal value.</param>
        /// <returns>Decimal value from configuration data.
        /// If decimal value is not found then default value will be returned.</returns>
        decimal GetDecimal(ConfigName key, decimal defaultValue);

        /// <summary>
        /// Gets configuration double value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default double value.</param>
        /// <returns>Double value from configuration data.
        /// If double value is not found then default value will be returned.</returns>
        double GetDouble(string key, double defaultValue);

        /// <summary>
        /// Gets configuration double value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default double value.</param>
        /// <returns>Double value from configuration data.
        /// If double value is not found then default value will be returned.</returns>
        double GetDouble(ConfigName key, double defaultValue);

        /// <summary>
        /// Gets configuration double value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Double value from configuration data.</returns>
        double GetDouble(string key);

        /// <summary>
        /// Gets configuration double value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Double value from configuration data.</returns>
        double GetDouble(ConfigName key);

        /// <summary>
        /// Sets configuration double value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified double value.</param>
        void SetDouble(string key, double value);

        /// <summary>
        /// Sets configuration double value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified double value.</param>
        void SetDouble(ConfigName key, double value);

        /// <summary>
        /// Gets configuration decimal value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Decimal value from configuration data.</returns>
        decimal GetDecimal(string key);

        /// <summary>
        /// Gets configuration decimal value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Decimal value from configuration data.</returns>
        decimal GetDecimal(ConfigName key);

        /// <summary>
        /// Sets configuration decimal value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified decimal value.</param>
        void SetDecimal(string key, decimal value);

        /// <summary>
        /// Sets configuration decimal value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified decimal value.</param>
        void SetDecimal(ConfigName key, decimal value);

        /// <summary>
        /// Gets configuration boolean value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default boolean value.</param>
        /// <returns>Boolean value from configuration data.
        /// If boolean value is not found then default value will be returned.</returns>
        bool GetBoolean(string key, bool defaultValue);

        /// <summary>
        /// Gets configuration boolean value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default boolean value.</param>
        /// <returns>Boolean value from configuration data.
        /// If boolean value is not found then default value will be returned.</returns>
        bool GetBoolean(ConfigName key, bool defaultValue);

        /// <summary>
        /// Gets configuration boolean value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Boolean value from configuration data.</returns>
        bool GetBoolean(string key);

        /// <summary>
        /// Gets configuration boolean value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Boolean value from configuration data.</returns>
        bool GetBoolean(ConfigName key);

        /// <summary>
        /// Sets configuration boolean value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified boolean value.</param>
        void SetBoolean(string key, bool value);

        /// <summary>
        /// Sets configuration boolean value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified boolean value.</param>
        void SetBoolean(ConfigName key, bool value);

        /// <summary>
        /// Gets configuration DateTime value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default DateTime value.</param>
        /// <returns>DateTime value from configuration data.
        /// If DateTime value is not found then default value will be returned.</returns>
        DateTime GetDateTime(string key, DateTime defaultValue);

        /// <summary>
        /// Gets configuration DateTime value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default DateTime value.</param>
        /// <returns>DateTime value from configuration data.
        /// If DateTime value is not found then default value will be returned.</returns>
        DateTime GetDateTime(ConfigName key, DateTime defaultValue);

        /// <summary>
        /// Gets configuration DateTime value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>DateTime value from configuration data.</returns>
        DateTime GetDateTime(string key);

        /// <summary>
        /// Gets configuration DateTime value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>DateTime value from configuration data.</returns>
        DateTime GetDateTime(ConfigName key);

        /// <summary>
        /// Sets configuration DateTime value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified DateTime value.</param>
        void SetDateTime(string key, DateTime value);

        /// <summary>
        /// Sets configuration DateTime value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified DateTime value.</param>
        void SetDateTime(ConfigName key, DateTime value);

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default binary value.</param>
        /// <returns>Binary value from configuration data.
        /// If binary value is not found then default value will be returned.</returns>
        byte[] GetBinary(string path, string key, byte[] defaultValue);

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default binary value.</param>
        /// <returns>Binary value from configuration data.
        /// If binary value is not found then default value will be returned.</returns>
        byte[] GetBinary(ConfigName path, string key, byte[] defaultValue);

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Binary value from configuration data.</returns>
        byte[] GetBinary(string path, string key);

        /// <summary>
        /// Gets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>Binary value from configuration data.</returns>
        byte[] GetBinary(ConfigName path, string key);

        /// <summary>
        /// Sets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified binary value.</param>
        void SetBinary(string path, string key, byte[] value);

        /// <summary>
        /// Sets configuration binary value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified binary value.</param>
        void SetBinary(ConfigName path, string key, byte[] value);

        /// <summary>
        /// Gets configuration Guid value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default Guid value.</param>
        /// <returns>Guid value from configuration data.
        /// If Guid value is not found then default value will be returned.</returns>
        Guid GetGuid(string key, Guid defaultValue);

        /// <summary>
        /// Gets configuration Guid value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">Default Guid value.</param>
        /// <returns>Guid value from configuration data.
        /// If Guid value is not found then default value will be returned.</returns>
        Guid GetGuid(ConfigName key, Guid defaultValue);

        /// <summary>
        /// Gets configuration Guid value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Guid value from configuration data.</returns>
        Guid GetGuid(string key);

        /// <summary>
        /// Gets configuration Guid value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Guid value from configuration data.</returns>
        Guid GetGuid(ConfigName key);

        /// <summary>
        /// Sets configuration Guid value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified Guid value.</param>
        void SetGuid(string key, Guid value);

        /// <summary>
        /// Sets configuration Guid value by specified key.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Specified Guid value.</param>
        void SetGuid(ConfigName key, Guid value);
    }
}