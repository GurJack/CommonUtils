using System;
using System.Diagnostics;

namespace CommonUtils.Extensions
{
    /// <summary>
    ///  Расширение StopWatch
    /// </summary>
    public static class ActionExtension
    {
        /// <summary>
        ///  Возврашает время затраченное на выполнение дейтсвия (через Stopwatch)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TimeSpan GetElapsedTime(this Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
