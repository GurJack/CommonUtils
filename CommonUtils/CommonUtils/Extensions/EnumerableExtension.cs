﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// Enumerable extension
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Convert IEnumerable to ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ObservableCollection<T> AsObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }

        /// <summary>
        /// Check if enumerable is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Fisher-Yates shuffle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            T[] elements = source.ToArray();
            var random = new Random();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = random.Next(i + 1);
                (elements[i], elements[swapIndex]) = (elements[swapIndex], elements[i]);
            }
            return elements;
        }

        /// <summary>
        /// Check if collection has items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool HasItems<T>(this IEnumerable<T>? source)
        {
            return source != null && source.Any();
        }

        /// <summary>
        /// Check if collection is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T>? source)
        {
            return source == null || !source.Any();
        }
    }
}
//                // ... except we don't really need to swap it fully, as we can
//                // return it immediately, and afterwards it's irrelevant.
//                int swapIndex = RandomGenerator.Next(i + 1);
//                yield return elements[swapIndex];
//                elements[swapIndex] = elements[i];
//            }
//        }

//        /// <summary>
//        /// Concatenates to string.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="source"></param>
//        /// <param name="selector"></param>
//        /// <param name="separator"></param>
//        /// <returns></returns>
//        public static string ToConcatenatedString<T>(this IEnumerable<T> source, Func<T, string> selector, string separator)
//        {
//            var b = new StringBuilder();
//            var needSeparator = false;

//            foreach (var item in source)
//            {
//                if (needSeparator)
//                    b.Append(separator);

//                b.Append(selector(item));
//                needSeparator = true;
//            }

//            return b.ToString();
//        }

//        /// <summary>
//        /// Concatenates to string.
//        /// </summary>
//        /// <param name="source"></param>
//        /// <param name="selector"></param>
//        /// <param name="separator"></param>
//        /// <returns></returns>
//        public static string ToConcatenatedString(this IEnumerable source, Func<object, string> selector, string separator)
//        {
//            var b = new StringBuilder();
//            var needSeparator = false;
//            var items = source.Flatten();

//            foreach (var item in items)
//            {
//                if (needSeparator)
//                    b.Append(separator);

//                b.Append(selector(item));
//                needSeparator = true;
//            }

//            return b.ToString();
//        }


//        /// <summary>
//        /// Converts to <see cref="LinkedList{T}"/>
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="source"></param>
//        /// <returns></returns>
//        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> source)
//        {
//            return new LinkedList<T>(source);
//        }

//        /// <summary>
//        /// Flattens array.
//        /// </summary>
//        /// <param name="list"></param>
//        /// <returns></returns>
//        public static List<object> Flatten(this IEnumerable list)
//        {
//            var ret = new List<object>();
//            if (list == null) return ret;

//            foreach (var item in list)
//            {
//                if (item == null) continue;

//                var arr = item as IEnumerable;
//                if (arr != null && !(item is string))
//                {
//                    ret.AddRange(arr.Cast<object>());
//                }
//                else
//                {
//                    ret.Add(item);
//                }
//            }
//            return ret;
//        }

//        /// <summary>
//        /// Gets the real type of collection.
//        /// </summary>
//        public static Type GetCollectionType(this IEnumerable collection)
//        {
//            if (collection == null) return null;

//            var type = collection.GetType();
//            if (type.IsGenericType)
//            {
//                var types = type.GetGenericArguments();
//                if (types.Length == 1)
//                {
//                    return types[0];
//                }
//                else
//                {
//                    // Could be null if implements two IEnumerable
//                    return type.GetInterfaces()
//                        .Where(t => t.IsGenericType)
//                        .SingleOrDefault(t => t.GetGenericTypeDefinition() == typeof(IEnumerable<>))?.GetGenericArguments()[0];
//                }
//            }
//            else if (collection.GetType().IsArray)
//            {
//                return type.GetElementType();
//            }


//            throw new ApplicationException($"Неизвестный тип: {type}");
//        }

//        /// <summary>
//        /// Foreach by any IEnumerable source.
//        /// </summary>
//        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
//        {
//            if (source == null)
//            {
//                return;
//            }

//            foreach (T obj in source)
//            {
//                action(obj);
//            }
//        }
//    }
//}
