using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using CommonUtils.Expressions;
using CommonUtils.Extensions;
using CommonUtils.Objects;

namespace CommonUtils
{
    /// <summary>
    /// Class for converting object types
    /// </summary>
    public static class ObjectConverter
    {
        /// <summary>
        /// Changes object value type
        /// </summary>
        /// <param name="value">The object value</param>
        /// <param name="type">The new object type</param>
        /// <returns>The new object value</returns>
        /// <exception cref="ArgumentNullException">type is null</exception>
        public static object ChangeType(object value, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (value == null)
            {
                return null;
            }

            if (value is DBNull)
            {
                return DBNull.Value;
            }

            if (type.IsInstanceOfType(value))
            {
                return value;
            }

            if (type.IsAssignableFrom(typeof (DateTime)))
            {
                switch (value)
                {
                    case byte byteValue:
                        return DateTime.FromBinary((long) byteValue);
                    case ulong ulongValue:
                        return DateTime.FromBinary((long) ulongValue);
                    case long longValue:
                        return DateTime.FromBinary(longValue);
                    default:
                        return DateTime.Parse(value.ToString());
                }
            }
            if (type.IsAssignableFrom(typeof (Decimal)))
            {
                //TODO костыль на различные форматы decimal
                if (value.ToString().Contains(","))
                {
                    return Decimal.Parse(value.ToString());
                }
                else
                {
                    return CF.ToNumeric(value.ToString());
                }
            }

            //cast IList to ObservableCollection:
            if (value is IList && type.IsGenericType && type.GetGenericTypeDefinition() == typeof (ObservableCollection<>))
                return CastToObservableCollection((IList) value, type.GetGenericArguments()[0]);

            //cast IList to List:
            if (value is IList && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return CastToList((IList)value, type.GetGenericArguments()[0]);

            if (value is String && type == typeof (LocalizableString))
                return new LocalizableString((string) value);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Determines whether two object values are equals
        /// </summary>
        /// <param name="firstValue">The first object value</param>
        /// <param name="secondValue">The second object value</param>
        /// <returns>Returns true if two objects are equals; otherwise, false.</returns>
        public new static bool Equals(object firstValue, object secondValue)
        {
            if (firstValue == secondValue)//null == null OR ReferenceEquals
            {
                return true;
            }

            if (firstValue == null || secondValue == null)
            {
                return false;
            }

            if (firstValue is ICollection && secondValue is ICollection)
            {
                return ListEquals((ICollection)firstValue, (ICollection)secondValue);
            }

            if (firstValue is LocalizableString && secondValue is LocalizableString)
            {
                return ((LocalizableString) firstValue).OriginalString == ((LocalizableString) secondValue).OriginalString;
            }

            if (firstValue is DatePeriodic && secondValue is DatePeriodic)
            {
                return ((DatePeriodic)firstValue).Begin == ((DatePeriodic)secondValue).Begin;
            }

            if (firstValue is Expression && secondValue is Expression)
            {
                return LambdaConverter.Equals((Expression) firstValue, (Expression) secondValue);
            }

            if (firstValue is Stream && secondValue is Stream)
            {
                return StreamEquals((Stream) firstValue, (Stream) secondValue);
            }

            try
            {
                EqualizeTypes(ref firstValue, ref secondValue);
            }
            catch
            {
                return false;
            }

            return Object.Equals(firstValue, secondValue);
        }
        
        /// <summary>
        /// Performs a case-sensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to or greater than the other.
        /// </summary>
        /// <param name="firstValue">The first object to compare.</param>
        /// <param name="secondValue">The second object to compare.</param>
        /// <returns>Less than zero if first is less than second. Zero if first equals second. Greater than zero if first is greater than second.</returns>
        public static int Compare(object firstValue, object secondValue)
        {
            if (firstValue == secondValue)
            {
                return 0;
            }

            if (!(firstValue is IComparable))
            {
                return -1;
            }

            if (!(secondValue is IComparable))
            {
                return 1;
            }

            try
            {
                EqualizeTypes(ref firstValue, ref secondValue);
            }
            catch
            {
                return -1;
            }

            return Comparer.DefaultInvariant.Compare(firstValue, secondValue);
        }

        /// <summary>
        /// Convert string to specific Type value via XmlConvert.
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static object FromString(string stringValue, Type valueType)
        {
            if (stringValue == String.Empty)
            {
                stringValue = null;
            }

            if (valueType.IsNullableType())
            {
                if (stringValue == null)
                {
                    return null;
                }

                valueType = Nullable.GetUnderlyingType(valueType);
            }

            if (valueType == typeof(Boolean))
            {
                if (stringValue == null)
                {
                    return false;
                }
                return XmlConvert.ToBoolean(stringValue);
            }
            
            if (valueType == typeof(Byte))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToByte(stringValue);
            }
            
            if (valueType == typeof(Byte[]))
            {
                if (stringValue == null)
                {
                    return new byte[0];
                }
                return Convert.FromBase64String(stringValue);
            }

            if (valueType == typeof(Char))
            {
                if (stringValue == null)
                {
                    return Char.MinValue;
                }
                return XmlConvert.ToChar(stringValue);
            }
            
            if (valueType == typeof(DateTime))
            {
                if (stringValue == null)
                {
                    return DateTime.MinValue;
                }
                return XmlConvert.ToDateTime(stringValue, XmlDateTimeSerializationMode.RoundtripKind);
            }

            if (valueType == typeof(DatePeriodic))
            {
                if (stringValue == null)
                {
                    return new DatePeriodic(DateTime.MinValue);
                }
                return new DatePeriodic(XmlConvert.ToDateTime(stringValue, XmlDateTimeSerializationMode.RoundtripKind));
            }

            if (valueType == typeof(DBNull))
            {
                return DBNull.Value;
            }
            if (valueType == typeof(Decimal))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToDecimal(stringValue);
            }
            
            if (valueType == typeof(Double))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToDouble(stringValue);
            }
            
            if (valueType == typeof(Guid))
            {
                if (stringValue == null)
                {
                    return Guid.Empty;
                }
                return XmlConvert.ToGuid(stringValue);
            }

            if (valueType == typeof(Int16))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToInt16(stringValue);
            }
            
            if (valueType == typeof(Int32))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToInt32(stringValue);
            }
            
            if (valueType == typeof(Int64))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToInt64(stringValue);
            }
            
            if (valueType == typeof(SByte))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToSByte(stringValue);
            }
            
            if (valueType == typeof(Single))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToSingle(stringValue);
            }
            
            if (valueType == typeof(String))
            {
                return stringValue;
            }

            if (valueType == typeof(LocalizableString))
            {
                return new LocalizableString(stringValue);
            }

            if (valueType == typeof(TimeSpan))
            {
                if (stringValue == null)
                {
                    return TimeSpan.Zero;
                }
                return XmlConvert.ToTimeSpan(stringValue);
            }

            if (valueType == typeof(UInt16))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToUInt16(stringValue);
            }
            
            if (valueType == typeof(UInt32))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToUInt32(stringValue);
            }
            
            if (valueType == typeof(UInt64))
            {
                if (stringValue == null)
                {
                    return 0;
                }
                return XmlConvert.ToUInt64(stringValue);
            }

            return null;
        }

        /// <summary>
        /// Determines whether value is empty
        /// </summary>
        /// <param name="value"></param>
        /// <param name="emptyValue"></param>
        /// <returns></returns>
        public static bool IsEmpty(object value, object emptyValue)
        {
            if (value is LocalizableString)
                return String.IsNullOrEmpty(((LocalizableString) value).OriginalString);

            return ObjectConverter.Equals(value, emptyValue);
        }

        /// <summary>
        /// Check for type compatibility.
        /// </summary>
        /// <param name="typeFrom"></param>
        /// <param name="typeTo"></param>
        /// <returns></returns>
        public static bool TypesIsCompatible(Type typeFrom, Type typeTo)
        {
            if (typeFrom == typeof(object)) return true;

            typeFrom = typeFrom.GetRealType();
            typeTo = typeTo.GetRealType();

            return
                typeFrom == typeTo ||
                (typeFrom == typeof(string) && typeTo == typeof(LocalizableString)) ||
                (typeFrom == typeof(LocalizableString) && typeTo == typeof(string)) ||
                (typeFrom == typeof(int) && typeTo == typeof(decimal)) ||
                (typeFrom == typeof(long) && typeTo == typeof(decimal)) ||
                (typeFrom == typeof(int) && typeTo == typeof(long)) ||
                (typeFrom == typeof(int) && typeTo == typeof(float)) ||
                (typeFrom == typeof(int) && typeTo == typeof(double));
        }


        /// <summary>
        /// TryParse and return converted value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryConvertType(object value, Type conversionType, out object result)
        {
            result = null;
            bool success = false;

            if (conversionType.IsInstanceOfType(value))
            {
                result = value;
                success = true;
            }

            else if (conversionType == typeof(Int16))
            {
                success = Int16.TryParse(value.ToString(), out Int16 res);
                if (success) result = res;
            }

            else if (conversionType == typeof(Int32))
            {
                success = Int32.TryParse(value.ToString(), out Int32 res);
                if (success) result = res;
            }

            else if (conversionType == typeof(Int64))
            {
                success = Int64.TryParse(value.ToString(), out Int64 res);
                if (success) result = res;
            }

            else if (conversionType == typeof(decimal))
            {
                success = Decimal.TryParse(value.ToString(), out decimal res);
                if (success) result = res;
            }

            else if (conversionType == typeof(double))
            {
                success = Double.TryParse(value.ToString(), out double res);
                if (success) result = res;
            }

            else if (conversionType == typeof(DateTime))
            {
                success = DateTime.TryParse(value.ToString(), out DateTime res);
                if (success) result = res;
            }

            else if (conversionType == typeof(string))
            {
                result = value;
                success = true;
            }

            return success;
        }

        private static void EqualizeTypes(ref object firstValue, ref object secondValue)
        {
            if (firstValue == secondValue
                || firstValue == null || firstValue is DBNull
                || secondValue == null || secondValue is DBNull)
            {
                return;
            }

            Type firstType = firstValue.GetType();
            Type secondType = secondValue.GetType();

            if (secondType.IsAssignableFrom(firstType)
                || firstType.IsAssignableFrom(secondType))
            {
                return;
            }

            if (firstValue is LocalizableString || secondValue is LocalizableString)
            {
                firstValue = firstValue is LocalizableString ? firstValue : new LocalizableString(Convert.ToString(firstValue, CultureInfo.InvariantCulture));
                secondValue = secondValue is LocalizableString ? secondValue : new LocalizableString(Convert.ToString(secondValue, CultureInfo.InvariantCulture));
            }
            else if (firstValue is DateTime && secondValue is String)
            {
                DateTime tmp;
                if (DateTime.TryParse((string) secondValue, out tmp))
                    secondValue = tmp;
            }
            else if (firstValue is String && secondValue is DateTime)
            {
                DateTime tmp;
                if (DateTime.TryParse((string) firstValue, out tmp))
                    firstValue = tmp;
            }
            else if (firstValue is Decimal || secondValue is Decimal)
            {
                firstValue = Convert.ToDecimal(firstValue, CultureInfo.InvariantCulture);
                secondValue = Convert.ToDecimal(secondValue, CultureInfo.InvariantCulture);
            }
            else if (firstValue is Int64 || secondValue is Int64)
            {
                firstValue = Convert.ToInt64(firstValue, CultureInfo.InvariantCulture);
                secondValue = Convert.ToInt64(secondValue, CultureInfo.InvariantCulture);
            }
            else if (firstValue is String || secondValue is String)
            {
                firstValue = Convert.ToString(firstValue, CultureInfo.InvariantCulture);
                secondValue = Convert.ToString(secondValue, CultureInfo.InvariantCulture);
            }


            Object.Equals(firstValue, secondValue);
        }

        private static bool ListEquals(ICollection firstList, ICollection secondList)
        {
            if (firstList == secondList)
            {
                return true;
            }

            if (firstList.Count != secondList.Count)
            {
                return false;
            }

            IEnumerator firstEnumerator = firstList.GetEnumerator();
            IEnumerator secondEnumerator = secondList.GetEnumerator();

            while (firstEnumerator.MoveNext()
                && secondEnumerator.MoveNext())
            {
                if (!Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool StreamEquals(Stream firstStream, Stream secondStream)
        {
            if (firstStream.Length != secondStream.Length)
                return false;

            firstStream.Position = 0;
            secondStream.Position = 0;

            using (var m1 = new MemoryStream())
            {
                firstStream.CopyTo(m1);
                m1.Position = 0;

                using (var m2 = new MemoryStream())
                {
                    secondStream.CopyTo(m2);
                    m2.Position = 0;

                    var msArray1 = m1.ToArray();
                    var msArray2 = m2.ToArray();

                    return msArray1.SequenceEqual(msArray2);//todo: сделать тест на производительность, что быстрее SequenceEqual OR ListEquals. По результатам везде использовать более быстрый вариант.
                }
            }
        }

        private static IList CastToObservableCollection(IList list, Type type)
        {
            var mt = GetMethodCastToObservableCollection.MakeGenericMethod(type);
            return (IList)mt.Invoke(list, new object[] { list });
        }

        private static IList CastToList(IList list, Type type)
        {
            var mt = GetMethodCastToList.MakeGenericMethod(type);
            return (IList)mt.Invoke(list, new object[] { list });
        }

        private static MethodInfo _getMethodCastToObservableCollection;
        private static MethodInfo _getMethodCastToList;

        private static MethodInfo GetMethodCastToObservableCollection
        {
            get { return _getMethodCastToObservableCollection ?? (_getMethodCastToObservableCollection = typeof (ObjectConverter).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).Where(x => string.Equals(x.Name, nameof(CastToObservableCollection), StringComparison.OrdinalIgnoreCase)).Single(x => x.GetParameters().Length == 1)); }
        }

        private static MethodInfo GetMethodCastToList
        {
            get { return _getMethodCastToList ?? (_getMethodCastToList = typeof(ObjectConverter).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).Where(x => string.Equals(x.Name, nameof(CastToList), StringComparison.OrdinalIgnoreCase)).Single(x => x.GetParameters().Length == 1)); }
        }

        private static ObservableCollection<T> CastToObservableCollection<T>(IList list)
        {
            return new ObservableCollection<T>(list.Cast<T>());
        }

        private static List<T> CastToList<T>(IList list)
        {
            //return (from object result in list select (T)result).ToList();
            return new List<T>(list.Cast<T>());
            //var realList = new List<T>(list.Count);
            //foreach(var val in list)
            //{
            //    var realVal = (T)val;
            //    realList.Add(realVal);
            //}

            //return realList;
        }
    }
}