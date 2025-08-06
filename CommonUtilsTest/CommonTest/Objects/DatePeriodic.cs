using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace CommonUtils.Objects
{
    /// <summary>
    /// DatePeriodic extensions
    /// </summary>
    public static class DatePeriodicExtensions
    {
        /// <summary>
        /// DateTime AsDatePeriodic
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns> 
        public static DatePeriodic AsDatePeriodic(this DateTime? s)
        {
            return new DatePeriodic(s);
        }

        /// <summary>
        /// DateTime AsDatePeriodic
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns> 
        public static DatePeriodic AsDatePeriodic(this DateTime s)
        {
            return new DatePeriodic(s);
        }
    }

    /// <summary>
    /// Class for attribute type
    /// </summary>
    [Serializable]
    [DataContract]
    public class DatePeriodic : IComparable, IConvertible, IComparable<DateTime?>, IComparable<DateTime>, IEquatable<DateTime?>, IEquatable<DateTime>
    {
        [IgnoreDataMember]
        private DateTime _begin;
        [IgnoreDataMember]
        private DateTime? _end;

        /// <summary>
        /// Begin of date periodic
        /// </summary>
        [DataMember]
        public DateTime Begin
        {
            get => _begin;
            set
            {
                if (value == DateTime.MinValue)
                    _begin = value;
                else if (value.Day != 1)
                    _begin = new DateTime(value.Year, value.Month, 1);
                else
                    _begin = value.Date;

                _end = null;
            }
        }

        /// <summary>
        /// End of date periodic
        /// </summary>
        [IgnoreDataMember]
        public DateTime End
        {
            get
            {
                if (!_end.HasValue)
                {
                    _end = Begin.AddMonths(1).AddMilliseconds(-2);
                }

                return _end.Value;
            }
            private set {}
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DatePeriodic() : this(null)
        {

        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="begin"></param>
        public DatePeriodic(DateTime? begin)
        {
            if (!begin.HasValue || begin.Value == DateTime.MinValue)
                Begin = DateTime.MinValue;
            else if (begin.Value.Day != 1)
                Begin = new DateTime(begin.Value.Year, begin.Value.Month, 1);
            else
                Begin = begin.Value.Date;
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public DatePeriodic(int year, int month)
        {
            Begin = new DateTime(year, month, 1);
        }

        /// <summary>
        /// User-defined conversion from DatePeriodic to DateTime? 
        /// </summary>
        /// <param name="datePeriodic"></param>
        /// <returns></returns>
        public static implicit operator DateTime?(DatePeriodic datePeriodic)
        {
            return datePeriodic.Begin;
        }

        /// <summary>
        /// User-defined conversion from DateTime? to DatePeriodic 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static implicit operator DatePeriodic(DateTime? dateTime)
        {
            return new DatePeriodic(dateTime);
        }

        /// <summary>
        /// User-defined conversion from DatePeriodic to DateTime 
        /// </summary>
        /// <param name="datePeriodic"></param>
        /// <returns></returns>
        public static implicit operator DateTime(DatePeriodic datePeriodic)
        {
            return datePeriodic.Begin;
        }

        /// <summary>
        /// User-defined conversion from DateTime to DatePeriodic 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static implicit operator DatePeriodic(DateTime dateTime)
        {
            return new DatePeriodic(dateTime);
        }

        /// <summary>
        /// User-defined operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(DatePeriodic a, DatePeriodic b)
        {
            return !ObjectConverter.Equals(a, b);
        }

        /// <summary>
        /// User-defined operator ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(DatePeriodic a, DatePeriodic b)
        {
            return ObjectConverter.Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DateTime other)
        {
            if (other == DateTime.MinValue && Begin == DateTime.MinValue) return 0;
            if (other == DateTime.MinValue) return 1;
            if (Begin == DateTime.MinValue) return -1;

            return Begin.CompareTo(other);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DateTime other)
        {
            return ObjectConverter.Equals(Begin, other);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current object.</returns>
        public override string ToString()
        {
            return Begin.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var dp = (DatePeriodic) obj;
            return (Begin == dp.Begin);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Begin.GetHashCode();
        }

        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
                return 1;

            var bObj = obj as DatePeriodic;
            if (bObj == null)
                throw new ArgumentException("Object is not of type DatePeriodic");

            return DateTime.Compare(Begin, bObj.Begin);
        }

        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DateTime? other)
        {
            if (other == null && Begin == DateTime.MinValue) return 0;
            if (other == null) return 1;
            if (Begin == DateTime.MinValue) return -1;

            return Begin.CompareTo(other);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DateTime? other)
        {
            return ObjectConverter.Equals(Begin, other);
        }

        /// <summary>
        /// GetTypeCode
        /// </summary>
        /// <returns></returns>
        public TypeCode GetTypeCode()
        {
            return Begin.GetTypeCode();
        }

        /// <summary>
        /// Convert to Boolean
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)Begin).ToBoolean(provider);
        }

        /// <summary>
        /// Convert to Char
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToChar(provider);
        }

        /// <summary>
        /// Convert to SByte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToSByte(provider);
        }

        /// <summary>
        /// Convert to Byte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToByte(provider);
        }

        /// <summary>
        /// Convert to Int16
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToInt16(provider);
        }

        /// <summary>
        /// Convert to UInt16
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToUInt16(provider);
        }

        /// <summary>
        /// Convert to Int32
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToInt32(provider);
        }

        /// <summary>
        /// Convert to UInt32
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToUInt32(provider);
        }

        /// <summary>
        /// Convert to Int64
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToInt64(provider);
        }

        /// <summary>
        /// Convert to UInt64
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToUInt64(provider);
        }

        /// <summary>
        /// Convert to Single
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToSingle(provider);
        }

        /// <summary>
        /// Convert to Double
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToDouble(provider);
        }

        /// <summary>
        /// Convert to Decimal
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToDecimal(provider);
        }

        /// <summary>
        /// Convert to DateTime
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToDateTime(provider);
        }

        /// <summary>
        /// Convert to String
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToString(provider);
        }

        /// <summary>
        /// Convert to Type
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible) Begin).ToType(conversionType, provider);
        }
    }
}