//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.Serialization;
//using CommonUtils.Extensions;

//namespace CommonUtils.Helpers
//{
//    /// <summary>
//    /// Type helper.
//    /// </summary>
//    public static class TypeHelper
//    {
//        /// <summary>
//        /// Adding known types.
//        /// </summary>
//        public static List<Type> AddKnownTypes<TParentType>(IEnumerable<Type> types, bool onlyDataContractTypes)
//        {
//            return AddTypes<TParentType>(types, onlyDataContractTypes);
//        }

//        /// <summary>
//        /// Adding known types from assemblies.
//        /// </summary>
//        public static List<Type> AddKnownTypes<TParentType>(IEnumerable<Assembly> assemblies, bool onlyDataContractTypes)
//        {
//            var types = new List<Type>();
//            foreach (var assembly in assemblies)
//            {
//                types.AddRange(AddKnownTypes<TParentType>(assembly, onlyDataContractTypes));
//            }

//            return types;
//        }

//        /// <summary>
//        /// Adding known types from assemblies.
//        /// </summary>
//        public static List<Type> AddKnownTypes<TParentType>(Assembly assembly, bool onlyDataContractTypes)
//        {
//            return AddTypes<TParentType>(assembly.GetLoadableTypes(), onlyDataContractTypes);
//        }

//        private static List<Type> AddTypes<TParentType>(IEnumerable<Type> types, bool onlyDataContractTypes)
//        {
//            Func<Type, bool> where;

//            var parentType = typeof (TParentType);
//            if (parentType.IsInterface)
//            {
//                where = f => f.GetInterfaces().Any(i => i == parentType) && f.IsClass && (!onlyDataContractTypes || f.GetCustomAttributes(typeof (DataContractAttribute), true).Any());
//            }
//            else if (parentType.IsClass)
//            {
//                where = f => f.IsSubclassOf(parentType) && (!onlyDataContractTypes || f.GetCustomAttributes(typeof (DataContractAttribute), true).Any());
//            }
//            else
//            {
//                throw new NotSupportedException($"NotSupported exception in {nameof(AddTypes)} for type <{parentType.FullName}>");
//            }

//            var tmpTypes = types.Where(where).ToList();

//            return tmpTypes;
//        }

//        /// <summary>
//        /// Gets interfaces from assemblies.
//        /// </summary>
//        public static List<Type> GetInrefaces<TParentInterface>(IEnumerable<Assembly> assemblies)
//        {
//            var types = new List<Type>();
//            foreach (var assembly in assemblies)
//            {
//                types.AddRange(GetInrefaces<TParentInterface>(assembly));
//            }

//            return types;
//        }

//        /// <summary>
//        /// Gets interfaces from assemblies.
//        /// </summary>
//        public static List<Type> GetInrefaces<TParentInterface>(Assembly assembly)
//        {
//            return GetInrefaces<TParentInterface>(assembly.GetLoadableTypes());
//        }

//        private static List<Type> GetInrefaces<TParentInterface>(IEnumerable<Type> types)
//        {
//            var parentType = typeof(TParentInterface);
//            var tmpTypes = types.Where(f => f.GetInterfaces().Any(i => i == parentType) && f.IsInterface).ToList();

//            return tmpTypes;
//        }

//        /// <summary>
//        /// Gets all assemlies in the specified directory name.
//        /// </summary>
//        /// <returns></returns>
//        public static List<Assembly> GetAllAssemblies()
//        {
//            return GetAllAssemblies(Information.ProgramPath);
//        }

//        /// <summary>
//        /// Gets all assemlies in the specified directory name.
//        /// </summary>
//        /// <param name="directoryName"></param>
//        /// <returns></returns>
//        public static List<Assembly> GetAllAssemblies(string directoryName)
//        {
//            var asm = Assembly.GetEntryAssembly();
//            //var allAssemblies = asm.GetReferencedAssemblies().Select(Assembly.Load).ToList();
//            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();


//            var files = System.IO.Directory.GetFiles(directoryName, "CIS.*.dll");
//            foreach (var file in files)
//            {
//                var assembly = ClassFactory.GetAssembly(file);
//                if (!allAssemblies.Contains(assembly))
//                    allAssemblies.Add(assembly);
//            }

//            return allAssemblies;
//        }

//        /// <summary>
//        /// Gets all types with specified parent.
//        /// </summary>
//        /// <returns></returns>
//        public static List<Type> GetAllTypes<T>(string directoryName, bool onlyDataContractTypes = true)
//        {
//            var allAssemblies = GetAllAssemblies(directoryName);
//            var types = TypeHelper.AddKnownTypes<T>(allAssemblies, onlyDataContractTypes);

//            return types;
//        }
//    }
//}