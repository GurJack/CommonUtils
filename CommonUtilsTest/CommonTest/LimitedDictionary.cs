using System;
using System.Collections.Generic;

namespace CommonUtils
{
    /// <summary>
    /// Dictionary with limited items.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class LimitedDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary;
        private readonly Queue<TKey> _keys;
        private readonly int _limit;
        private readonly int _countToRemove;
        private object _syncRoot;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="limit"></param>
        public LimitedDictionary(int limit)
        {
            _keys = new Queue<TKey>(limit);
            _limit = limit;
            _countToRemove = Math.Max(1, (int) (0.1 * _limit));
            _dictionary = new Dictionary<TKey, TValue>(limit);
        }

        /// <summary>
        /// Add item to dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (_dictionary.Count == _limit)
            {
                for (int i = 0; i < _countToRemove; i++)
                {
                    var oldestKey = _keys.Dequeue();
                    _dictionary.Remove(oldestKey);
                }
            }

            _dictionary.Add(key, value);
            _keys.Enqueue(key);
        }

        /// <summary>
        /// Gets the item by the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key] => _dictionary[key];

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the LimitedDictionary.
        /// </summary>
        public object SyncRoot => _syncRoot ?? (_syncRoot = new object());
    }
}