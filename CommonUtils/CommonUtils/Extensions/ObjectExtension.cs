﻿﻿﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// Object extensions
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Check if object is in values list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In<T>(this T obj, params T[] values)
        {
            return values.Any(p => Equals(obj, p));
        }

        /// <summary>
        /// Check if object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object? obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Check if object is not null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object? obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Get caller information
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="memberName"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public static string WhoseThere(this object obj, [CallerMemberName] string memberName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            return $"Метод - {memberName}, файл - {fileName}, строка - {lineNumber}";
        }

        /// <summary>
        /// Map object to target type
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        public static TTarget MapTo<TTarget>(this object obj) where TTarget : class, new()
        {
            TTarget result = (TTarget) MapTo(obj, obj.GetType(), typeof(TTarget));
            return result;
        }

        private static object MapTo(object source, Type sourceType, Type targetType)
        {
            var result = Activator.CreateInstance(targetType);
            // Simple mapping logic would go here
            return result;
        }

        /// <summary>
        /// Convert object to JSON string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            if (obj == null) return "null";
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Return default value if object is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T IfNull<T>(this T? obj, T defaultValue) where T : class
        {
            return obj ?? defaultValue;
        }

        /// <summary>
        /// Return default value if string is null
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IfNull(this string? str, string defaultValue)
        {
            return str ?? defaultValue;
        }

        /// <summary>
        /// Clone object using JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CloneObject<T>(this T obj) where T : class
        {
            if (obj == null)
                return null!;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json)!;
        }

        /// <summary>
        /// Safe cast to target type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T SafeCast<T>(this object obj)
        {
            try
            {
                if (obj is T result)
                    return result;

                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return default(T)!;
            }
        }

        /// <summary>
        /// Get property value by name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object? GetPropertyValue(this object obj, string propertyName)
        {
            if (obj == null || string.IsNullOrEmpty(propertyName))
                return null;

            var property = obj.GetType().GetProperty(propertyName);
            return property?.GetValue(obj);
        }
    }
}
//        {
//            var result = Activator.CreateInstance(targetType);
//            var type = typeof(MapNameAttribute);
//            foreach (PropertyInfo property in sourceType.GetProperties()
//                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == type)))
//            {
//                var attribute = Attribute.GetCustomAttribute(property, type);
//                if (attribute == null)
//                    throw new ApplicationException($"Не найден атрибут: {type.Name}");
//                var resProperty = result.GetType().GetProperty(((MapNameAttribute) attribute).Name);
//                if (resProperty == null)
//                    throw new ApplicationException(
//                        $"Не найдено поле: {((MapNameAttribute) attribute).Name} в классе {targetType.Name}");
//                if (property.PropertyType.FullName == resProperty.PropertyType.FullName)
//                    resProperty.SetValue(result, property.GetValue(source));
//                else if (resProperty.PropertyType.FullName == "System.String")
//                    resProperty.SetValue(result, property.GetValue(source).ToString());
//                else
//                {
//                    var newProp = property.GetValue(source);
//                    if (!typeof(IList).IsAssignableFrom(property.PropertyType))
//                        resProperty.SetValue(result, MapTo(newProp, property.PropertyType, resProperty.PropertyType));
//                    else
//                    {
//                        var sType = property.PropertyType.IsArray
//                            ? property.PropertyType.GetElementType()
//                            : property.PropertyType.GetGenericArguments()[0];
//                        Type tType;
//                        IList list;
//                        if (resProperty.PropertyType.IsArray)
//                        {
//                            tType = resProperty.PropertyType.GetElementType();
//                            list = (IList) Activator.CreateInstance(resProperty.PropertyType,
//                                new object[] {((IList) newProp).Count});
//                            for (int i = 0; i < ((IList) newProp).Count; i++)
//                            {
//                                list[i] = MapTo(((IList) newProp)[i], sType, tType);
//                            }

//                        }
//                        else
//                        {
//                            tType = resProperty.PropertyType.GetGenericArguments()[0];
//                            list = (IList) Activator.CreateInstance(resProperty.PropertyType);
//                            foreach (var item in (IList) newProp)
//                            {
//                                list.Add(MapTo(item, sType, tType));

//                            }
//                        }

//                        resProperty.SetValue(result, list);
//                    }
//                }
//            }

//            return result;
//        }
//    }
//}
