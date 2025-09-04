using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.InteropServices;

namespace CommonUtils.Security
{
    /// <summary>
    /// Помощник для работы с безопасными строками (SecureString)
    /// </summary>
    public static class SecurePasswordHelper
    {
        /// <summary>
        /// Преобразует обычную строку в безопасную строку
        /// </summary>
        /// <param name="password">Пароль в виде обычной строки</param>
        /// <returns>Безопасная строка или null, если входной параметр пустой</returns>
        public static SecureString? ConvertToSecureString(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;

            var secureString = new SecureString();
            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }

        /// <summary>
        /// Преобразует безопасную строку в обычную строку
        /// </summary>
        /// <param name="secureString">Безопасная строка</param>
        /// <returns>Обычная строка или null, если входной параметр null</returns>
        public static string? ConvertToString(SecureString? secureString)
        {
            if (secureString == null) return null;

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
                }
            }
        }
    }
}
