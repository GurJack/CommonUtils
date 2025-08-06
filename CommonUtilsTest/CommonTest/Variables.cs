//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Runtime.CompilerServices;
//using CommonUtils.ConfigProviders;

//namespace CommonUtils
//{
//    /// <summary>
//    /// Static class for getting application variables
//    /// </summary>
//    public static class Variables
//    {
//        /// <summary>
//        /// Config section for variables settings.
//        /// </summary>
//        private const string ConfigPath = ConfigProvider.System + "Settings/Variables";

//        private static readonly Dictionary<string, string> ValueHash = new Dictionary<string, string>();

//        /// <summary>
//        /// DefaultLogoImageName
//        /// </summary>
//        public const string DefaultLogoImageName = "LogoImageNY";

//        /// <summary>
//        /// DefaultIconName
//        /// </summary>
//        public const string DefaultIconName = "MainIcon";

//        /// <summary>
//        /// DefaultIconSmallName
//        /// </summary>
//        public const string DefaultIconSmallName = "MainIcon_16x16";

//        private static Icon _mainIcon;
//        private static Icon _mainIconSmall;


//        /// <summary>
//        /// Main program icon
//        /// </summary>
//        public static Icon MainIcon
//        {
//            get
//            {
//                if (_mainIcon == null)
//                {
//                    if (string.IsNullOrEmpty(MainIconName))
//                    {
//                        MainIconName = DefaultIconName;
//                    }

//                    _mainIcon = ImageProvider.GetIcon(MainIconName);
//                }

//                return _mainIcon;
//            }
//        }

//        /// <summary>
//        /// Main program icon (16x16).
//        /// </summary>
//        public static Icon MainIconSmall
//        {
//            get
//            {
//                if (_mainIconSmall == null)
//                {
//                    if (string.IsNullOrEmpty(MainIconSmallName))
//                    {
//                        MainIconSmallName = DefaultIconSmallName;
//                    }

//                    _mainIconSmall = ImageProvider.GetIcon(MainIconSmallName);
//                }

//                return _mainIconSmall;
//            }
//        }

//        /// <summary>
//        /// Gets the developers e-mail address.
//        /// </summary>
//        public static string DeveloperEmail
//        {
//            get { return GetValue(); }
//            set { SetValue(nameof(DeveloperEmail), value); }
//        }

//        /// <summary>
//        /// User e-mail address.
//        /// </summary>
//        public static string UserEmail
//        {
//            get { return GetValue(); }
//            set { SetValue(nameof(UserEmail), value); }
//        }

//        /// <summary>
//        /// Gets or sets the logo image name.
//        /// </summary>
//        public static string LogoImageName
//        {
//            get { return GetValue(); }
//            set { SetValue(nameof(LogoImageName), value); }
//        }

//        /// <summary>
//        /// Gets or sets the main icon name.
//        /// </summary>
//        public static string MainIconName
//        {
//            get { return GetValue(); }
//            set
//            {
//                _mainIcon = null;
//                SetValue(nameof(MainIconName), value);
//            }
//        }

//        /// <summary>
//        /// Gets or sets the main small icon name.
//        /// </summary>
//        public static string MainIconSmallName
//        {
//            get { return GetValue(); }
//            set
//            {
//                _mainIconSmall = null;
//                SetValue(nameof(MainIconSmallName), value);
//            }
//        }

//        /// <summary>
//        /// Gets the specified variable value.
//        /// </summary>
//        /// <param name="name">The variable name.</param>
//        /// <returns>The variable value.</returns>
//        public static string GetValue([CallerMemberName] string name = null)
//        {
//            if (name == null)
//            {
//                throw new ArgumentNullException(nameof(name));
//            }

//            lock (((ICollection) ValueHash).SyncRoot)
//            {
//                if (ValueHash.ContainsKey(name))
//                    return ValueHash[name];
//            }

//            var value = ConfigProvider.GetString(ConfigPath, name);
//            if (value != null)
//            {
//                lock (((ICollection) ValueHash).SyncRoot)
//                {
//                    ValueHash[name] = value;
//                }
//                return value;
//            }


//            return null;
//        }

//        /// <summary>
//        /// Sets the specified variable value.
//        /// </summary>
//        /// <param name="name">The variable name.</param>
//        /// <param name="value">The variable value.</param>
//        public static void SetValue(string name, string value)
//        {
//            if (name == null)
//            {
//                throw new ArgumentNullException(nameof(name));
//            }

//            lock (((ICollection) ValueHash).SyncRoot)
//            {
//                ValueHash[name] = value;

//                ConfigProvider.SetString(ConfigPath, name, value);
//            }
//        }
//    }
//}