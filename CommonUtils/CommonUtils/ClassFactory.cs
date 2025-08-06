using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.DataContracts;

namespace CommonUtils
{
    /// <summary>
    /// Static class for working with a dinamic libraries.
    /// </summary>
    public static class ClassFactory
    {
        // Key is assembly name + "," + type name. Value is type.
        private static readonly ConcurrentDictionary<string, Type> _typeHash = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Gets the assembly by the assembly name.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <returns>The assembly instance.</returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }

            if (assemblyName == "mscorlib")
            {
                return typeof (System.Object).Assembly;
            }

            if (assemblyName.Length == 0)
            {
                return null;
            }

            if (assemblyName.Contains(Path.DirectorySeparatorChar))
                return Assembly.LoadFrom(assemblyName);

            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyName);
            if (assembly != null)
                return assembly;

            return Assembly.Load(new AssemblyName(assemblyName));
        }

        /// <summary>
        /// Gets the type by the type name.
        /// </summary>
        /// <param name="typeName">The full type name.</param>
        /// <returns>The type instance.</returns>
        public static Type GetType(string typeName)
        {
            if (typeName == null)
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            var key = typeName;

            if (!_typeHash.TryGetValue(key, out var type))
            {
                type = _typeHash[key] = Type.GetType(typeName, false);
            }

            return type;
        }

        /// <summary>
        /// Gets the type by the type name.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="typeName">The full type name.</param>
        /// <returns>The type instance.</returns>
        public static Type GetType(string assemblyName, string typeName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            if (typeName == null)
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            string key = assemblyName + "," + typeName;

            if (!_typeHash.TryGetValue(key, out var type))
            {
                var assembly = GetAssembly(assemblyName);
                if (assembly == null)
                {
                    return null;
                }

                type = _typeHash[key] = assembly.GetType(typeName);
            }

            return type;
        }

        /// <summary>
        /// Creates the instance of the specified type name.
        /// </summary>
        /// <param name="typeName">The full type name.</param>
        /// <param name="args">The constructor parameters.</param>
        /// <returns>The new object instance.</returns>
        public static object CreateInstance(string typeName, params object[] args)
        {
            if (typeName == null)
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            if (args == null
                || args.Length == 0)
            {
                return CreateInstance(GetType(typeName));
            }

            return CreateInstance(GetType(typeName), args);
        }

        /// <summary>
        /// Creates the instance of the specified type in the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The specified assembly name.</param>
        /// <param name="typeName">The full type name.</param>
        /// <param name="args">The constructor parameters.</param>
        /// <returns>The new object instance.</returns>
        public static object CreateInstance(string assemblyName, string typeName, params object[] args)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            if (typeName == null)
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            if (args == null
                || args.Length == 0)
            {
                return CreateInstance(GetType(assemblyName, typeName));
            }

            return CreateInstance(GetType(assemblyName, typeName), args);
        }

        /// <summary>
        /// Creates the instance of the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CreateInstance(Type type, params object[] args)
        {
            if (args == null
                || args.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            if (type.IsGenericTypeDefinition)
            {
                var typeArgs = new Type[type.GetGenericArguments().Length];
                Array.Copy(args, typeArgs, typeArgs.Length);
                type = type.MakeGenericType(typeArgs);

                var newArgs = new object[args.Length - typeArgs.Length];
                Array.Copy(args, typeArgs.Length, newArgs, 0, newArgs.Length);
                args = newArgs;
            }

            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// Creates the instance of the specified type
        /// </summary>
        /// <typeparam name="T">The specified type</typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(params object[] args)
        {
            return (T) CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// Get types in specified assembly by namespace.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        /// <summary>
        /// Get types in specified assembly assignable from the type.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static Type[] GetTypesForParent(Assembly assembly, Type parentType)
        {
            return assembly.GetTypes().Where(t => t.IsAssignableFrom(parentType)).ToArray();
        }

        public static T CastObject<T>(object input) where T : class
        {
            return (T)input;
        }

        public static T ConvertObject<T>(object input) where T : class
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }

        //public static void GetProperties(Type type)
        //{
        //    foreach (var propertyInfo in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
        //    {

        //    }
        //}
    }
}