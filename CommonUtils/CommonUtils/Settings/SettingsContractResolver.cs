using CommonUtils.Security;
using CommonUtils.Settings.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;

namespace CommonUtils.Settings
{
    public class SettingsContractResolver : DefaultContractResolver
    {
        private readonly string _encryptionKey;

        public SettingsContractResolver(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            // Игнорируем свойства с атрибутом DoNotSaveToFile
            if (member.GetCustomAttribute<DoNotSaveToFileAttribute>() != null)
            {
                property.Ignored = true;
            }

            // Шифрование значений
            var cryptAttr = member.GetCustomAttribute<CryptAttribute>();
            if (cryptAttr?.IsCrypt == true)
            {
                property.ValueProvider = new EncryptedValueProvider(property.ValueProvider, _encryptionKey);
            }

            return property;
        }

        private class EncryptedValueProvider : IValueProvider
        {
            private readonly IValueProvider _innerProvider;
            private readonly string _encryptionKey;

            public EncryptedValueProvider(IValueProvider innerProvider, string encryptionKey)
            {
                _innerProvider = innerProvider;
                _encryptionKey = encryptionKey;
            }

            public object GetValue(object target)
            {
                var value = _innerProvider.GetValue(target) as string;
                if (string.IsNullOrEmpty(value)) return value;

                try
                {
                    return SecureEncryptionHelper.Encrypt(value, _encryptionKey);
                }
                catch
                {
                    return value; // Возвращаем исходное значение при ошибке
                }
            }

            public void SetValue(object target, object value)
            {
                if (value is string encrypted)
                {
                    try
                    {
                        value = SecureEncryptionHelper.Decrypt(encrypted, _encryptionKey);
                    }
                    catch
                    {
                        // Оставляем зашифрованное значение при ошибке
                    }
                }
                _innerProvider.SetValue(target, value);
            }
        }
    }
}
