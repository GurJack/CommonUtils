using System;
using System.Collections.Generic;
using System.Linq;


namespace CommonUtils.ExcelManager
{
    public static class ExcelLib
    {
        
        public static object[,] ReadToEnd(string path)
        {
            //TODO: Закоментил чтобы ошибку не исправлять, раскоментить
            //    using (var reader = new OpenXmlExcelReader(path))
            //    {
            //        var result = reader.ReadAll();
            //        return result;
            //    }
            throw new NotImplementedException("Закоментил чтобы ошибку не исправлять, раскоментить");
        }

        public static void Do(this object[,] result, Action<object[]> action) =>
            Map(result, p =>
            {
                action(p);
                return 0;
            });

        public static IEnumerable<T> Map<T>(this object[,] result, Func<object[], T> func)
        {

            var attemps = 0;
            var n = result.GetUpperBound(0);
            var m = result.GetUpperBound(1);
            var arr = new object[m];
            var list = new List<T>();
            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                    arr.SetValue(result[i, j], j - 1);
                var obj = default(T);
                try
                {
                    obj = func(arr);
                }
                catch (Exception e)
                {
                    if (attemps > 0)
                    {
                        var msg = $"Не смог преобразовать строку в объект\n{e.Message}\nстрока {i}\n{arr.Aggregate("", (p, q) => p + q + " | ")}";
                        throw new InvalidCastException(msg, e);
                    }
                    attemps++;
                }
                if (!Equals(obj, default(T)))
                    list.Add(obj);
            }

            return list;
        }
    }
}