using System;
using System.Collections.Concurrent;
using System.Linq;

namespace CommonUtils.Expressions
{
    /// <summary>
    /// Gets or sets external values.
    /// </summary>
    public static class ExternalValues
    {
        private static readonly ConcurrentDictionary<string, object> _valueDic = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Gets the specified external value.
        /// </summary>
        /// <param name="name">The value name.</param>
        /// <returns>The external value.</returns>
        public static object Get(string name)
        {
            object res;
            _valueDic.TryGetValue(name, out res);

            return res;
        }

        /// <summary>
        /// Sets the specified external value.
        /// </summary>
        /// <param name="name">The value name.</param>
        /// <param name="value">The external value.</param>
        public static void Set(string name, object value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            _valueDic.AddOrUpdate(name, value, (key, oldvalue) => value);
        }

        /// <summary>
        /// Removes the specified external value.
        /// </summary>
        /// <param name="name">The value name.</param>
        public static void Remove(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            object res;
            _valueDic.TryRemove(name, out res);
        }

        /// <summary>
        /// Removes the specified external value.
        /// </summary>
        /// <param name="startName">The value start name.</param>
        public static void RemoveByStartName(string startName)
        {
            if (startName == null)
            {
                throw new ArgumentNullException("name");
            }

            var keysToRemove = _valueDic.Where(r => r.Key.StartsWith(startName)).Select(r=>r.Key).ToList();

            foreach (var key in keysToRemove)
            {
                object res;
                _valueDic.TryRemove(key, out res);
            }
        }
    }
}