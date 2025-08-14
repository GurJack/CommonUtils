//using System.Collections.Generic;

//namespace CommonUtils.Extensions
//{
//    /// <summary>
//    /// Extension methods for IDictionary types
//    /// </summary>
//    public static class DictionaryExtension
//    {
//        /// <summary>
//        /// Returns the default value if the value by key was not found
//        /// </summary>
//        /// <typeparam name="TKey"></typeparam>
//        /// <typeparam name="TValue"></typeparam>
//        /// <param name="dict"></param>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
//        {
//            return dict.TryGetValue(key, out var value) ? value : default(TValue);
//        }

//        /// <summary>
//        /// Adds range to original dictionary.
//        /// </summary>
//        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> addingDict)
//        {
//            foreach (var val in addingDict)
//            {
//                dict.Add(val.Key, val.Value);
//            }
//        }
//    }
//}
