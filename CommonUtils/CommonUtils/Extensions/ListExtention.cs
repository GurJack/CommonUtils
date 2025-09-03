//using System.Collections.Generic;
//using System.Linq;

//namespace CommonUtils.Extensions
//{
//    /// <summary>
//    /// The class of list extentions.
//    /// </summary>
//    public static class ListExtention
//    {
//        /// <summary>
//        /// Move the element from old position to new position.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="source"></param>
//        /// <param name="oldIndex"></param>
//        /// <param name="newIndex"></param>
//        public static void Move<T>(this List<T> source, int oldIndex, int newIndex)
//        {
//            var item = source[oldIndex];
//            source.RemoveAt(oldIndex);
//            source.Insert(newIndex, item);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="list"></param>
//        /// <param name="chunkSize"></param>
//        /// <returns></returns>
//        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int chunkSize)
//        {
//            for(int i = 0; i < list.Count(); i++)
//            {
//                yield return list.Take(chunkSize);
//                list = list.Skip(chunkSize);
//            }
//        }
//    }
//}