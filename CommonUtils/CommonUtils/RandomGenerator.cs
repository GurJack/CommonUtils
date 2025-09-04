using System;
using System.Text;

namespace CommonUtils
{
    /// <summary>
    /// Represents a pseudo-random number generator.
    /// </summary>
    public static class RandomGenerator
    {
        private static readonly Random _random = new Random((int) (DateTime.Now.Ticks & 0x0000FFFF)/10);

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than System.Int32.MaxValue.</returns>
        public static int Next()
        {
            lock (_random)
            {
                return _random.Next();
            }
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue
        /// must be greater than or equal to zero.</param>
        /// <returns> A 32-bit signed integer greater than or equal to zero, and less than maxValue;
        /// that is, the range of return values ordinarily includes zero but not maxValue.
        /// However, if maxValue equals zero, maxValue is returned.</returns>
        public static int Next(int maxValue)
        {
            lock (_random)
            {
                return _random.Next(maxValue);
            }
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue. If
        /// minValue equals maxValue, minValue is returned.</returns>
        public static int Next(int minValue, int maxValue)
        {
            lock (_random)
            {
                return _random.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// Generate random string of specified length
        /// </summary>
        /// <param name="length">String length</param>
        /// <returns>Random string</returns>
        public static string GenerateString(int length)
        {
            if (length <= 0) return string.Empty;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new StringBuilder(length);

            lock (_random)
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(chars[_random.Next(chars.Length)]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Generate random integer in range
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>Random integer</returns>
        public static int GenerateInt(int min, int max)
        {
            return Next(min, max + 1);
        }

        /// <summary>
        /// Generate random double in range
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>Random double</returns>
        public static double GenerateDouble(double min, double max)
        {
            lock (_random)
            {
                return min + (_random.NextDouble() * (max - min));
            }
        }

        /// <summary>
        /// Generate random boolean
        /// </summary>
        /// <returns>Random boolean</returns>
        public static bool GenerateBool()
        {
            return Next(2) == 1;
        }

        /// <summary>
        /// Generate random GUID
        /// </summary>
        /// <returns>Random GUID</returns>
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Generate random DateTime in range
        /// </summary>
        /// <param name="minDate">Min date</param>
        /// <param name="maxDate">Max date</param>
        /// <returns>Random DateTime</returns>
        public static DateTime GenerateDateTime(DateTime minDate, DateTime maxDate)
        {
            var range = maxDate - minDate;
            var randomTicks = (long)(GenerateDouble(0, 1) * range.Ticks);
            return minDate.AddTicks(randomTicks);
        }

        /// <summary>
        /// Generate random email
        /// </summary>
        /// <returns>Random email</returns>
        public static string GenerateEmail()
        {
            var username = GenerateAlphaString(8);
            var domain = GenerateAlphaString(6);
            var tld = GenerateAlphaString(3);
            return $"{username}@{domain}.{tld}";
        }

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <param name="length">Password length</param>
        /// <returns>Random password</returns>
        public static string GeneratePassword(int length)
        {
            if (length < 4) throw new ArgumentException("Password length must be at least 4");

            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*";

            var result = new StringBuilder(length);

            lock (_random)
            {
                // Ensure at least one character from each category
                result.Append(upperChars[_random.Next(upperChars.Length)]);
                result.Append(lowerChars[_random.Next(lowerChars.Length)]);
                result.Append(digitChars[_random.Next(digitChars.Length)]);
                result.Append(specialChars[_random.Next(specialChars.Length)]);

                // Fill the rest
                var allChars = upperChars + lowerChars + digitChars + specialChars;
                for (int i = 4; i < length; i++)
                {
                    result.Append(allChars[_random.Next(allChars.Length)]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Generate random alphabetic string
        /// </summary>
        /// <param name="length">String length</param>
        /// <returns>Random alphabetic string</returns>
        public static string GenerateAlphaString(int length)
        {
            if (length <= 0) return string.Empty;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder(length);

            lock (_random)
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(chars[_random.Next(chars.Length)]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Generate random numeric string
        /// </summary>
        /// <param name="length">String length</param>
        /// <returns>Random numeric string</returns>
        public static string GenerateNumericString(int length)
        {
            if (length <= 0) return string.Empty;

            const string chars = "0123456789";
            var result = new StringBuilder(length);

            lock (_random)
            {
                for (int i = 0; i < length; i++)
                {
                    result.Append(chars[_random.Next(chars.Length)]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns array of bytes with random numbers.
        /// </summary>
        /// <param name="length">Array length.</param>
        /// <returns>An array of bytes to contain random numbers.</returns>
        public static byte[] NextBytes(int length)
        {
            var bytes = new byte[length];
            lock (_random)
            {
                _random.NextBytes(bytes);
            }
            return bytes;
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
        public static double NextDouble()
        {
            lock (_random)
            {
                return _random.NextDouble();
            }
        }

        /// <summary>
        /// Returns string with random chars.
        /// </summary>
        /// <param name="length">String length.</param>
        /// <returns>A string with random chars.</returns>
        public static string NextString(int length)
        {
            var result = Convert.ToBase64String(NextBytes(length + 4));
            return result.Substring(0, length);
        }
    }
}
