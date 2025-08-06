using System;
using System.Linq;
using System.Reflection;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// Assembly extensions
    /// </summary>
    public static class AssemblyExtension
    {
        /// <summary>
        /// Get loadable types.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Type[] GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null).ToArray();
            }
        }
    }
}