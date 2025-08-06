using System.Collections;
using System.Collections.ObjectModel;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// ObservableCollection extensions
    /// </summary>
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// AddRange to ObservableCollection
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static void AddRange<T>(this ObservableCollection<T> target, IList sourceList)
        {
            if (sourceList == null || sourceList.Count == 0) return;

            foreach (var source in sourceList)
            {
                target.Add((T)source);
            }
        }

        /// <summary>
        /// AddRange to ObservableCollection
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static void AddRange<T>(this ObservableCollection<T> target, ObservableCollection<T> sourceList)
        {
            if (sourceList == null || sourceList.Count == 0) return;

            foreach (var source in sourceList)
            {
                target.Add(source);
            }
        }
    }
}