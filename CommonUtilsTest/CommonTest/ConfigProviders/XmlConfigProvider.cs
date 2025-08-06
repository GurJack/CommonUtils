using System;
using System.IO;
using System.Xml;

namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// Class for getting and setting custom configuration data.
    /// Loads the file on constructor and saves the file on Save method.
    /// </summary>
    public class XmlConfigProvider : ICacheableConfigProvider
    {
        private const string DefaultConfig = @"<?xml version=""1.0"" encoding=""utf-8""?>
<config format=""CISConfig"" version=""1"">
</config>";

        private const string SectionName = "section";
        private const string KeyNodeName = "key";
        private const string NameAttributeName = "name";

        private XmlDocument _config = null;
        private bool _changed = false;

        /// <summary>
        /// Constructor with configuration file name.
        /// Loads the configuration file.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        public XmlConfigProvider(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            FileName = fileName;

            CreateDefaultConfig();
            Load();
        }

        /// <summary>
        /// Gets the configuration file name.
        /// </summary>
        public virtual string FileName { get; }

        /// <summary>
        /// Loads the configuration file.
        /// </summary>
        public virtual void Load()
        {
            if (IsAvailableRead)
            {
                try
                {
                    using (var stream = ReadData())
                    {
                        _config.Load(stream);
                    }
                }
                catch (XmlException ex)
                {
                    throw new Exception(String.Format(CommonMessages.InvalidXmlConfigFile, FileName), ExceptionManager.GetRealException(ex));
                }
            }
        }

        /// <summary>
        /// Gets available to read flag.
        /// </summary>
        protected virtual Boolean IsAvailableRead => FileProvider.ExistsFile(FileName);
        

        /// <summary>
        /// Reads the configuration data.
        /// </summary>
        /// <returns></returns>
        protected virtual Stream ReadData()
        {
            return FileProvider.ReadFile(FileName);
        }

        /// <summary>
        /// Saves the configuration file.
        /// </summary>
        public virtual void Save()
        {
            if (!_changed)
            {
                return;
            }

            PreSave();
            WriteData();

            _changed = false;
        }

        /// <summary>
        /// Any specified action before writes configuration information to storage.
        /// </summary>
        protected virtual void PreSave()
        {
            FileProvider.ClearFile(FileName);
        }

        /// <summary>
        /// Writes the configuration data.
        /// </summary>
        protected virtual void WriteData()
        {
            using (var stream = FileProvider.WriteFile(FileName))
            {
                ConfigTo(stream);
            }
        }

        /// <summary>
        /// Saves the XML document to the specified stream.
        /// </summary>
        /// <param name="stream">The specified stream.</param>
        protected void ConfigTo(Stream stream)
        {
            _config.Save(stream);
        }

        /// <summary>
        /// Сбрасывает все настройки
        /// </summary>
        public void Reset()
        {
            CreateDefaultConfig();
        }

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

            var startPath = UriHelper.GetTail(path);

            // Find root.
            var rootNode = GetRootNode();
            if (rootNode == null)
            {
                return null;
            }

            // Find section.
            var sectionNode = GetSectionNode(rootNode, startPath);
            if (sectionNode == null)
            {
                return null;
            }

            // Find key.
            var keyNode = GetKeyNode(sectionNode, key);

            // Get data.
            var valueNode = (XmlText) keyNode?.FirstChild;

            return valueNode?.Data;
        }

        /// <summary>
        /// Gets configuration string value by specified path and key.
        /// </summary>
        /// <param name="path">Path to configuration key.</param>
        /// <param name="key">Configuration key.</param>
        /// <returns>String value from configuration data.</returns>
        public string GetString(ConfigName path, string key)
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

            var startPath = UriHelper.GetTail(path);

            // Find root.
            var rootNode = GetRootNode();
            if (rootNode == null)
            {
                return;
            }

            // Find and create section.
            var sectionNode = CreateSectionNode(rootNode, startPath);
            if (sectionNode == null)
            {
                throw new ArgumentException(String.Format(CommonMessages.InvalidConfigPath, startPath));
            }

            // Find key.
            var keyNode = GetKeyNode(sectionNode, key);

            // Delete key.
            if (value == null)
            {
                if (keyNode != null)
                {
                    keyNode.ParentNode.RemoveChild(keyNode);
                    _changed = true;
                }
                return;
            }

            // Create key.
            if (keyNode == null)
            {
                keyNode = _config.CreateElement(KeyNodeName);
                keyNode = (XmlElement) sectionNode.AppendChild(keyNode);
                keyNode.SetAttribute(NameAttributeName, key);
            }

            // Set value.
            var valueNode = (XmlText) keyNode.FirstChild;
            if (valueNode == null)
            {
                valueNode = _config.CreateTextNode(value);
                valueNode = (XmlText) keyNode.AppendChild(valueNode);
            }
            else
            {
                valueNode.Data = value;
            }

            _changed = true;
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


        private void CreateDefaultConfig()
        {
            _config = new XmlDocument();
            _config.LoadXml(DefaultConfig);
        }

        private XmlElement GetRootNode()
        {
            var childNodeList = _config.ChildNodes;
            foreach (XmlNode childNode in childNodeList)
            {
                if (childNode is XmlElement
                    && childNode.Name == "config")
                {
                    return (XmlElement) childNode;
                }
            }

            return null;
        }

        private XmlElement GetSectionNode(XmlElement parentNode, string path)
        {
            if (parentNode == null)
            {
                return null;
            }

            if (path.Length == 0)
            {
                return parentNode;
            }

            string section;
            string newPath;

            var separatorIndex = path.IndexOf(UriHelper.PathSeparator);
            if (separatorIndex >= 0)
            {
                section = path.Substring(0, separatorIndex);
                newPath = path.Substring(separatorIndex + 1, path.Length - separatorIndex - 1);
            }
            else
            {
                section = path;
                newPath = String.Empty;
            }

            XmlElement sectionNode = null;
            var childNodeList = parentNode.ChildNodes;
            foreach (XmlNode childNode in childNodeList)
            {
                if (childNode is XmlElement
                    && childNode.Name == SectionName
                    && ((XmlElement) childNode).GetAttribute(NameAttributeName) == section)
                {
                    sectionNode = (XmlElement) childNode;
                    break;
                }
            }

            if (sectionNode == null)
            {
                return null;
            }

            if (newPath.Length == 0)
            {
                return sectionNode;
            }

            return GetSectionNode(sectionNode, newPath);
        }

        private XmlElement CreateSectionNode(XmlElement parentNode, string path)
        {
            if (parentNode == null)
            {
                return null;
            }

            if (path.Length == 0)
            {
                return parentNode;
            }

            string section;
            string newPath;

            var separatorIndex = path.IndexOf(UriHelper.PathSeparator);
            if (separatorIndex >= 0)
            {
                section = path.Substring(0, separatorIndex);
                newPath = path.Substring(separatorIndex + 1, path.Length - separatorIndex - 1);
            }
            else
            {
                section = path;
                newPath = String.Empty;
            }

            XmlElement sectionNode = null;
            var childNodeList = parentNode.ChildNodes;
            foreach (XmlNode childNode in childNodeList)
            {
                if (childNode is XmlElement
                    && childNode.Name == SectionName
                    && ((XmlElement) childNode).GetAttribute(NameAttributeName) == section)
                {
                    sectionNode = (XmlElement) childNode;
                    break;
                }
            }

            if (sectionNode == null)
            {
                sectionNode = _config.CreateElement(SectionName);
                sectionNode.SetAttribute(NameAttributeName, section);
                sectionNode = (XmlElement) parentNode.AppendChild(sectionNode);
            }

            if (newPath.Length == 0)
            {
                return sectionNode;
            }
            return CreateSectionNode(sectionNode, newPath);
        }

        private XmlElement GetKeyNode(XmlElement sectionNode, string key)
        {
            if (sectionNode == null
                || key.Length == 0)
            {
                return null;
            }

            var childNodeList = sectionNode.ChildNodes;
            foreach (XmlNode childNode in childNodeList)
            {
                if (childNode is XmlElement
                    && childNode.Name == KeyNodeName
                    && ((XmlElement) childNode).GetAttribute(NameAttributeName) == key)
                {
                    return (XmlElement) childNode;
                }
            }

            return null;
        }
    }
}