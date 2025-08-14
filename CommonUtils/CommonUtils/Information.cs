//using System;
//using System.IO;
//using System.Linq;
//using System.Reflection;

//namespace CommonUtils
//{
//    /// <summary>
//    /// Static class for getting application specific information.
//    /// </summary>
//    public static class Information
//    {
//        /// <summary>
//        /// Developer company name.
//        /// </summary>
//        public static string CompanyName
//        {
//            get
//            {
//                var entryAssembly = GetEntryAssembly();
//                if (entryAssembly != null)
//                {
//                    foreach (AssemblyCompanyAttribute attribute in entryAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false))
//                    {
//                        if (attribute.Company != null)
//                        {
//                            return attribute.Company;
//                        }
//                    }
//                }

//                return "UnknownCompany";
//            }
//        }

//        /// <summary>
//        /// Application name.
//        /// </summary>
//        public static string ProductName
//        {
//            get
//            {
//                var entryAssembly = GetEntryAssembly();
//                if (entryAssembly != null)
//                {
//                    foreach (AssemblyProductAttribute attribute in entryAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false))
//                    {
//                        if (attribute.Product != null)
//                        {
//                            return attribute.Product;
//                        }
//                    }
//                }

//                return "UnknownProduct";
//            }
//        }

//        /// <summary>
//        /// Application name, major version info.
//        /// </summary>
//        public static string ProductNameWithMajorVersion => ProductName + " " + ProductVersion.Major + "." + ProductVersion.MajorRevision;

//        /// <summary>
//        /// Application version.
//        /// </summary>
//        public static Version ProductVersion
//        {
//            get
//            {
//                var entryAssembly = GetEntryAssembly();
//                if (entryAssembly == null)
//                {
//                    return new Version();
//                }

//                var attribute = (AssemblyInformationalVersionAttribute) entryAssembly.
//                    GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).FirstOrDefault();

//                if (attribute != null)
//                {
//                    var actualVersion = attribute.InformationalVersion;
//                    var spaceIndex = actualVersion.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase);
//                    if (spaceIndex >= 0)
//                    {
//                        actualVersion = actualVersion.Remove(spaceIndex);
//                    }
//                    return new Version(actualVersion);
//                }

//                return entryAssembly.GetName().Version;
//            }
//        }

//        /// <summary>
//        /// Application version, date.
//        /// </summary>
//        public static String ProductVersionWithDate
//        {
//            get
//            {
//                var entryAssembly = GetEntryAssembly();
//                if (entryAssembly == null)
//                {
//                    return new Version().ToString();
//                }

//                var attribute = (AssemblyInformationalVersionAttribute)entryAssembly.
//                    GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).FirstOrDefault();

//                if (attribute != null)
//                {
//                    return attribute.InformationalVersion;
//                }

//                return entryAssembly.GetName().Version.ToString();
//            }
//        }

//        /// <summary>
//        /// Copiright information.
//        /// </summary>
//        public static string Copyright
//        {
//            get
//            {
//                var entryAssembly = GetEntryAssembly();
//                if (entryAssembly != null)
//                {
//                    foreach (AssemblyCopyrightAttribute attribute in entryAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false))
//                    {
//                        if (attribute.Copyright != null)
//                        {
//                            return attribute.Copyright;
//                        }
//                    }
//                }

//                return String.Empty;
//            }
//        }

//        /// <summary>
//        /// Full path of executable file.
//        /// </summary>
//        public static string ProgramLocation
//        {
//            get
//            {
//                var name = "Unknown.exe";
//                var entryAssembly = GetEntryAssembly();
//                if (entryAssembly != null)
//                {
//                    name = Path.GetFileName(entryAssembly.Location);
//                }
//                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);
//            }
//        } 

//        /// <summary>
//        /// Name of executable file.
//        /// </summary>
//        public static string ProgramFile => Path.GetFileName(ProgramLocation);

//        /// <summary>
//        /// Directory of executable file.
//        /// </summary>
//        public static string ProgramPath => Path.GetDirectoryName(ProgramLocation);

//        /// <summary>
//        /// Executable file name without extension.
//        /// </summary>
//        public static string ProgramName => Path.GetFileNameWithoutExtension(ProgramFile);

//        /// <summary>
//        /// Current user path for storage application specific data.
//        /// </summary>
//        public static string ProgramDataPath
//        {
//            get
//            {
//                var path = Path.Combine(
//                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
//                    Path.Combine(CompanyName, ProductName));

//                if (!Directory.Exists(path))
//                {
//                    Directory.CreateDirectory(path);
//                }

//                return path;
//            }
//        }

//        /// <summary>
//        /// Current user path for application temporary storage.
//        /// </summary>
//        public static string ProgramTempPath
//        {
//            get
//            {
//                var path = Path.Combine(
//                    Path.GetTempPath(),
//                    Path.Combine(CompanyName, ProductName));

//                if (!Directory.Exists(path))
//                {
//                    Directory.CreateDirectory(path);
//                }

//                return path;
//            }
//        }

//        private static Assembly GetEntryAssembly()
//        {
//            return Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
//        }

//        public static string UserName => Environment.UserName;
//        public static string MachineName => Environment.MachineName;
//    }
//}