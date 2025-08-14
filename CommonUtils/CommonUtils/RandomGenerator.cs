//using System;

//namespace CommonUtils
//{
//    /// <summary>
//    /// Represents a pseudo-random number generator.
//    /// </summary>
//    public static class RandomGenerator
//    {
//        private static readonly Random _random = new Random((int) (DateTime.Now.Ticks & 0x0000FFFF)/10);

//        /// <summary>
//        /// Returns a nonnegative random number.
//        /// </summary>
//        /// <returns>A 32-bit signed integer greater than or equal to zero and less than System.Int32.MaxValue.</returns>
//        public static int Next()
//        {
//            lock (_random)
//            {
//                return _random.Next();
//            }
//        }

//        /// <summary>
//        /// Returns a nonnegative random number less than the specified maximum.
//        /// </summary>
//        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue
//        /// must be greater than or equal to zero.</param>
//        /// <returns> A 32-bit signed integer greater than or equal to zero, and less than maxValue;
//        /// that is, the range of return values ordinarily includes zero but not maxValue.
//        /// However, if maxValue equals zero, maxValue is returned.</returns>
//        public static int Next(int maxValue)
//        {
//            lock (_random)
//            {
//                return _random.Next(maxValue);
//            }
//        }

//        /// <summary>
//        /// Returns a random number within a specified range.
//        /// </summary>
//        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
//        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
//        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue;
//        /// that is, the range of return values includes minValue but not maxValue. If
//        /// minValue equals maxValue, minValue is returned.</returns>
//        public static int Next(int minValue, int maxValue)
//        {
//            lock (_random)
//            {
//                return _random.Next(minValue, maxValue);
//            }
//        }

//        /// <summary>
//        /// Returns array of bytes with random numbers.
//        /// </summary>
//        /// <param name="length">Array length.</param>
//        /// <returns>An array of bytes to contain random numbers.</returns>
//        public static byte[] NextBytes(int length)
//        {
//            var bytes = new byte[length];
//            lock (_random)
//            {
//                _random.NextBytes(bytes);
//            }
//            return bytes;
//        }

//        /// <summary>
//        /// Returns a random number between 0.0 and 1.0.
//        /// </summary>
//        /// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
//        public static double NextDouble()
//        {
//            lock (_random)
//            {
//                return _random.NextDouble();
//            }
//        }

//        /// <summary>
//        /// Returns string with random chars.
//        /// </summary>
//        /// <param name="length">String length.</param>
//        /// <returns>A string with random chars.</returns>
//        public static string NextString(int length)
//        {
//            var result = Convert.ToBase64String(NextBytes(length + 4));
//            return result.Substring(0, length);
//        }
//    }
//}