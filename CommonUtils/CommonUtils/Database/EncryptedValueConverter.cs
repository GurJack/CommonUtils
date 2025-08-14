using CommonUtils.Security;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace CommonUtils.Database
{
    public class EncryptedValueConverter : ValueConverter<string, string>
    {
        public EncryptedValueConverter()
            : base(
                v => EncryptValue(v),
                v => DecryptValue(v))
        {
        }

        private static string EncryptValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return SecureEncryptionHelper.Encrypt(value, GlobalConstant.APIKey);
        }

        private static string DecryptValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return SecureEncryptionHelper.Decrypt(value, GlobalConstant.APIKey);
        }
    }
}
