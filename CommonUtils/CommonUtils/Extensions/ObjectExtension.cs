//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using CommonUtils.Attributes;

//namespace CommonUtils.Extensions
//{
//    public static class ObjectExtension
//    {
//        public static bool In<T>(this T obj, params T[] values)
//        {
//            return values.Any(p => Equals(obj, p));
//        }

//        public static string WhoseThere(this object obj, [CallerMemberName] string memberName = "",
//            [CallerFilePath] string fileName = "",
//            [CallerLineNumber] int lineNumber = 0)
//        {
//            return $"Метод - {memberName}, файл - {fileName}, строка - {lineNumber}";
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <typeparam name="TTarget"></typeparam>
//        /// <returns></returns>
//        public static TTarget MapTo<TTarget>(this object obj) where TTarget : class, new()
//        {
//            TTarget result = (TTarget) MapTo(obj, obj.GetType(), typeof(TTarget));
//            return result;
//        }

//        private static object MapTo(object source, Type sourceType, Type targetType)
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
