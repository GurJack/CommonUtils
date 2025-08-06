using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CommonUtils.Event;
using CommonUtils.Extensions;
using CommonUtils.Objects;

namespace CommonUtils.Expressions
{
    /// <summary>
    /// Class contains collection of the standard functions.
    /// CF - CommonFunctions
    /// </summary>
    public static partial class CF
    {
        private static readonly Dictionary<string, ProcedureCallbackWithParams> _functions =
            new Dictionary<string, ProcedureCallbackWithParams>();

        /// <summary>
        /// Setting function.
        /// </summary>
        public static ProcedureCallbackWithParams Setting => GetFunction(nameof(Setting));

        /// <summary>
        /// GetLanguage function.
        /// </summary>
        public static ProcedureCallbackWithParams GetLanguage => GetFunction(nameof(GetLanguage));

        /// <summary>
        /// Returns function by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ProcedureCallbackWithParams GetFunction(string name)
        {
            return _functions[name];
        }

        /// <summary>
        /// Sets function.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="procedure"></param>
        public static void SetFunction(string name, ProcedureCallbackWithParams procedure)
        {
            _functions.Add(name, procedure);
        }

        /// <summary>
        /// ShortDateString
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortDateString(DateTime? date)
        {
            if (!date.HasValue) return String.Empty;

            return date.Value.ToShortDateString();
        }

        /// <summary>
        /// DateString
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateString(DateTime? date)
        {
            if (!date.HasValue) return String.Empty;

            return date.Value.ToLongDateString();
        }

        /// <summary>
        /// CurrentDateTime
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// CurrentDate
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentDate()
        {
            return DateTime.UtcNow.Date;
        }

        /// <summary>
        /// Convert date to UTC
        /// </summary>
        /// <returns>Date in UTC</returns>
        public static DateTime ToUTC(DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }

        /// <summary>
        /// CurrentDatePeriodic
        /// </summary>
        /// <returns></returns>
        public static DatePeriodic CurrentDatePeriodic()
        {
            return new DatePeriodic(DateTime.Now.Date);
        }

        /// <summary>
        /// Modify from UTC date time value to local time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? ToLocalTime(DateTime? date)
        {
            return date?.ToLocalTime();
        }

        /// <summary>
        /// Convert string to decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToNumeric(string value)
        {
            return Decimal.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert string to integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(string value)
        {
            return Int32.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// IsNullOrEmpty for LocalizableString
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(LocalizableString s)
        {
            return String.IsNullOrEmpty(s);
        }

        /// <summary>
        /// IsNullOrEmpty for String
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(String s)
        {
            return String.IsNullOrEmpty(s);
        }

        /// <summary>
        /// IsEmpty for Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(Guid s)
        {
            return Guid.Empty == s;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(object value)
        {
            if (value == null) return String.Empty;

            return value.ToString();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(Guid? value)
        {
            if (value == null) return String.Empty;

            return value.ToString();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(decimal? value)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString("### ### ### ### ##0.### ###").Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(decimal? value, string format)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString(format).Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(double? value)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString("### ### ### ### ##0.### ###").Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(double? value, string format)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString(format).Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(long? value, string format)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString(format).Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(int? value, string format)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString(format).Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(int? value)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString("### ### ### ### ##0.### ###").Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(long? value)
        {
            if (value == null) return String.Empty;

            return value.Value.ToString("### ### ### ### ##0.### ###").Trim();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string ToString(ICollection collection)
        {
            return ToString(collection, ",");
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToString(ICollection collection, string separator)
        {
            if (collection == null) return String.Empty;

            var sb = new StringBuilder();
            var first = true;
            foreach (var item in collection)
            {
                if (!first)
                    sb.Append(separator);

                sb.Append(item);

                first = false;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert to sql string as Date
        /// </summary>
        public static string DateToSqlString(DateTime? value)
        {
            return string.Format("{0:yyyy-MM-dd}", value ?? DateTime.MinValue);
        }

        /// <summary>
        /// Convert to sql string as DateTime
        /// </summary>
        public static string DateTimeToSqlString(DateTime? value)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", value ?? DateTime.MinValue);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToExpressionString(object value)
        {
            return ToExpressionString(value, 1);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subQueryLevel"></param>
        /// <returns></returns>
        public static string ToExpressionString(object value, int subQueryLevel)
        {
            return CommonExpression.PrepareValueForExpression(value, subQueryLevel);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToExpressionString(Guid? value)
        {
            return ToExpressionString(value, 1);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subQueryLevel"></param>
        /// <returns></returns>
        public static string ToExpressionString(Guid? value, int subQueryLevel)
        {
            return ToExpressionString((object) value, subQueryLevel);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToExpressionString(decimal? value)
        {
            return ToExpressionString(value, 1);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subQueryLevel"></param>
        /// <returns></returns>
        public static string ToExpressionString(decimal? value, int subQueryLevel)
        {
            return ToExpressionString((object) value, subQueryLevel);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToExpressionString(int? value)
        {
            return ToExpressionString(value, 1);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subQueryLevel"></param>
        /// <returns></returns>
        public static string ToExpressionString(int? value, int subQueryLevel)
        {
            return ToExpressionString((object) value, subQueryLevel);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToExpressionString(long? value)
        {
            return ToExpressionString(value, 1);
        }

        /// <summary>
        /// To string for Expression using.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subQueryLevel"></param>
        /// <returns></returns>
        public static string ToExpressionString(long? value, int subQueryLevel)
        {
            return ToExpressionString((object) value, subQueryLevel);
        }

        /// <summary>
        /// Convert number to words.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="male"></param>
        /// <param name="seniorOne"></param>
        /// <param name="seniorTwo"></param>
        /// <param name="seniorFive"></param>
        /// <param name="juniorOne"></param>
        /// <param name="juniorTwo"></param>
        /// <param name="juniorFive"></param>
        /// <param name="firstToUpper"></param>
        /// <param name="addRemain"></param>
        /// <returns></returns>
        public static string NumberToWords(double number, bool male = true,
            string seniorOne = "", string seniorTwo = "", string seniorFive = "",
            string juniorOne = "", string juniorTwo = "", string juniorFive = "",
            bool firstToUpper = true,
            bool addRemain = false) //TODO: локализовать
        {
            bool minus = false;
            if (number < 0)
            {
                number = -number;
                minus = true;
            }

            int n = (int) number;
            int remainder = (int) ((number - n + 0.005) * 100);

            var r = new StringBuilder();

            if (0 == n)
                r.Append("0 ");

            if (n % 1000 != 0)
                r.Append(NumberToWordsHelper.Str(n, male, seniorOne, seniorTwo, seniorFive));
            else
                r.Append(seniorFive);

            n /= 1000;

            r.Insert(0, NumberToWordsHelper.Str(n, false, "тысяча", "тысячи", "тысяч"));
            n /= 1000;

            r.Insert(0, NumberToWordsHelper.Str(n, true, "миллион", "миллиона", "миллионов"));
            n /= 1000;

            r.Insert(0, NumberToWordsHelper.Str(n, true, "миллиард", "миллиарда", "миллиардов"));
            n /= 1000;

            r.Insert(0, NumberToWordsHelper.Str(n, true, "триллион", "триллиона", "триллионов"));
            n /= 1000;

            r.Insert(0, NumberToWordsHelper.Str(n, true, "триллиард", "триллиарда", "триллиардов"));
            if (minus)
                r.Insert(0, "минус ");

            if (addRemain)
            {
                r.Append(remainder.ToString("00 "));
                r.Append(NumberToWordsHelper.Case(remainder, juniorOne, juniorTwo, juniorFive));
            }

            if (firstToUpper)
                r[0] = char.ToUpper(r[0]);

            return r.ToString().Trim();
        }

        /// <summary>
        /// Convert number to words in Prepositional.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="word1"></param>
        /// <param name="wordmore"></param>
        /// <param name="firstToUpper"></param>
        /// <returns></returns>
        public static string NumberToWordsPrepositional(int number, string word1 = "", string wordmore = "",
            bool firstToUpper = true) //TODO: локализовать
        {
            //получаем число прописью
            string str = NumberToWordsHelper.NumPhrase(number, true) + " ";

            //добавляем единицы
            byte endpart = (byte) (number % 100);
            if (endpart > 19)
                endpart = (byte) (endpart % 10);
            if (endpart == 1)
                str += word1;
            else
                str += wordmore;

            str = str.Trim();

            if (firstToUpper)
                str = char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);

            return str;
        }

        /// <summary>
        /// ФИО в формате "Фамилия Инициалы"
        /// </summary>
        /// <param name="fullName">Full name</param>
        /// <returns></returns>
        public static string GetFullNameAbbr(string fullName)
        {
            if (String.IsNullOrEmpty(fullName))
                return String.Empty;
            if (fullName.Contains("(") && fullName.Contains(")"))
                fullName = fullName.Replace(
                    fullName.Substring(fullName.IndexOf("("), fullName.IndexOf(")") - fullName.IndexOf("(") + 1), "");
            return fullName.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Aggregate((x, y) => x += " " + y[0] + ".");
        }

        /// <summary>
        ///  Converts the DateTime to the DatePeriodic equivalent.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DatePeriodic DatePeriodicParse(DateTime dateTime)
        {
            return new DatePeriodic(dateTime);
        }

        /// <summary>
        ///  Converts the string representation of a date and time to the DatePeriodic equivalent.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DatePeriodic DatePeriodicParse(string s)
        {
            return new DatePeriodic(DateTime.Parse(s));
        }

        /// <summary>
        ///  Converts the string representation of a date and time to the DatePeriodic equivalent.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DatePeriodic DatePeriodicParse(object value)
        {
            if (value == null) return new DatePeriodic();

            if (value is DateTime) return DatePeriodicParse((DateTime) value);

            if (value is string) return DatePeriodicParse((string) value);

            throw new NotSupportedException();
        }

        /// <summary>
        /// Add arguments as decimal values.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Add(params object[] args)
        {
            var res = 0m;
            foreach (var a in args)
            {
                if (a == null) continue;

                res += Convert.ToDecimal(a, CultureInfo.InvariantCulture);
            }

            return res;
        }

        /// <summary>
        /// Get date from nullable DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDate(DateTime? dateTime)
        {
            return dateTime?.Date ?? DateTime.MinValue;
        }

        public static DateTime SetDate(int year, int month = 1, int day = 1)
        {
            return new DateTime(year, month, day).Date;
        }

        /// <summary>
        /// Difference between dates in days
        /// </summary>
        /// <param name="newDate"></param>
        /// <param name="oldDate"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime? newDate, DateTime? oldDate)
        {
            return Math.Abs((GetDate(newDate) - GetDate(oldDate)).Days);
        }

        /// <summary>
        /// Difference between dates in hours
        /// </summary>
        /// <param name="newDate"></param>
        /// <param name="oldDate"></param>
        /// <returns></returns>
        public static double DateTimeDiff(DateTime? newDate, DateTime? oldDate)
        {
            return Math.Abs((GetDate(newDate) - GetDate(oldDate)).TotalHours);
        }

        /// <summary>
        /// Get Date from string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object date)
        {
            if (date is DateTime)
                return (DateTime) date;
            return DateTime.MinValue;
        }

    }
}